using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    [DllImport("libcob-4", EntryPoint = "cob_init", CallingConvention = CallingConvention.Cdecl)]
    public static extern void CobInit(int argc, IntPtr argv);

    [DllImport("consulta.dll", EntryPoint = "consulta", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ConsultarCliente(byte[] buffer);

    [DllImport("alteracao.dll", EntryPoint = "alteracao", CallingConvention = CallingConvention.Cdecl)]
    public static extern void AlterarCliente(byte[] buffer);

    [DllImport("exclusao.dll", EntryPoint = "exclusao", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ExcluirCliente(byte[] buffer);

    [DllImport("inclusao.dll", EntryPoint = "inclusao", CallingConvention = CallingConvention.Cdecl)]
    public static extern void IncluirCliente(byte[] buffer);

    static void Main(string[] args)
    {
        string msysBinPath = @"C:\msys64\mingw64\bin";
        if (Directory.Exists(msysBinPath))
        {
            string currentPath = Environment.GetEnvironmentVariable("PATH") ?? "";
            Environment.SetEnvironmentVariable("PATH", msysBinPath + Path.PathSeparator + currentPath);
        }

        Console.Clear();
        Console.WriteLine("[Ambiente] Inicializando engine do GnuCOBOL (libcob-4)...");
        try
        {
            CobInit(0, IntPtr.Zero);
            Console.WriteLine("[Ambiente] Runtime integrado com sucesso.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Falha Crítica] Nao foi possivel carregar a engine do COBOL: {ex.Message}");
            return;
        }

        while (true)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("      COOPERATIVA FINANCEIRA ALFA - INTEGRADA       ");
            Console.WriteLine("====================================================");
            Console.WriteLine("1. Consultar Cliente");
            Console.WriteLine("2. Alterar Cliente (Telefone/E-mail)");
            Console.WriteLine("3. Excluir Cliente");
            Console.WriteLine("4. Cadastrar Novo Cliente");
            Console.WriteLine("5. Sair");
            Console.WriteLine("====================================================");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            if (opcao == "5") break;
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4")
            {
                Console.WriteLine("\n[ALERTA] Opcao invalida! Selecione de 1 a 5.");
                PausarTela();
                continue;
            }

            Console.Write("\nDigite o Código do Cliente (exatamente 9 dígitos): ");
            string codigo = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(codigo) || codigo.Length != 9 || !long.TryParse(codigo, out _))
            {
                Console.WriteLine("\n[ERRO DE VALIDAÇÃO] O codigo deve conter exatamente 9 digitos numericos.");
                PausarTela();
                continue;
            }

            byte[] buffer = new byte[96];
            Array.Fill(buffer, (byte)' ');
            byte[] codigoBytes = Encoding.ASCII.GetBytes(codigo);
            Array.Copy(codigoBytes, 0, buffer, 0, 9);

            if (opcao == "1")
            {
                try
                {
                    ConsultarCliente(buffer);
                    ExibirBufferConsulta(buffer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FALHA] Erro de Comunicacao na Leitura: {ex.Message}");
                }
            }
            else if (opcao == "2")
            {
                Console.Write("Digite o Novo Telefone: ");
                string tel = Console.ReadLine()?.Trim();
                Console.Write("Digite o Novo E-mail: ");
                string email = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("\n[ERRO DE VALIDAÇÃO] Telefone e E-mail nao podem ser vazios.");
                    PausarTela();
                    continue;
                }

                for (int i = 39; i < 54; i++) buffer[i] = (byte)' ';
                for (int i = 54; i < 94; i++) buffer[i] = (byte)' ';

                byte[] telBytes = Encoding.ASCII.GetBytes(tel);
                Array.Copy(telBytes, 0, buffer, 39, Math.Min(telBytes.Length, 15));

                byte[] emailBytes = Encoding.ASCII.GetBytes(email);
                Array.Copy(emailBytes, 0, buffer, 54, Math.Min(emailBytes.Length, 40));

                try
                {
                    AlterarCliente(buffer);
                    string status = Encoding.ASCII.GetString(buffer, 94, 2).Trim();

                    if (status == "00")
                        Console.WriteLine("\n[SUCESSO] Cadastro atualizado com sucesso no legado!");
                    else
                        Console.WriteLine("\n[ALERTA] Falha na atualizacao. Cliente nao localizado (Status 01).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FALHA CRÍTICA] Erro ao gravar dados: {ex.Message}");
                }
            }
            else if (opcao == "3")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CONFIRMAÇÃO: Tem certeza que deseja EXCLUIR este cliente? (S/N): ");
                Console.ResetColor();
                string confirmacao = Console.ReadLine()?.Trim().ToUpper();

                if (confirmacao != "S")
                {
                    Console.WriteLine("\n[OPERACAO CANCELADA] Remoção abortada pelo usuário.");
                    PausarTela();
                    continue;
                }

                try
                {
                    ExcluirCliente(buffer);
                    string status = Encoding.ASCII.GetString(buffer, 94, 2).Trim();

                    if (status == "00")
                        Console.WriteLine("\n[SUCESSO] Cliente removido definitivamente do sistema legado!");
                    else
                        Console.WriteLine("\n[ALERTA] Falha na exclusao. Cliente nao localizado (Status 01).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FALHA CRÍTICA] Erro na rotina de exclusao nativa: {ex.Message}");
                }
            }
            else if (opcao == "4")
            {
                Console.Write("Digite o Nome do Novo Cliente: ");
                string nome = Console.ReadLine()?.Trim();
                Console.Write("Digite o Telefone: ");
                string tel = Console.ReadLine()?.Trim();
                Console.Write("Digite o E-mail: ");
                string email = Console.ReadLine()?.Trim();

                // Validacao de usabilidade na camada moderna antes de tocar a memoria nativa
                if (string.IsNullOrEmpty(nome))
                {
                    Console.WriteLine("\n[ERRO DE VALIDAÇÃO] O Nome do cliente é um campo de preenchimento obrigatório.");
                    PausarTela();
                    continue;
                }

                // Higienização de todas as fatias do buffer estruturado de dados
                for (int i = 9; i < 39; i++) buffer[i] = (byte)' ';
                for (int i = 39; i < 54; i++) buffer[i] = (byte)' ';
                for (int i = 54; i < 94; i++) buffer[i] = (byte)' ';

                byte[] nomeBytes = Encoding.ASCII.GetBytes(nome);
                Array.Copy(nomeBytes, 0, buffer, 9, Math.Min(nomeBytes.Length, 30));

                byte[] telBytes = Encoding.ASCII.GetBytes(tel ?? "");
                Array.Copy(telBytes, 0, buffer, 39, Math.Min(telBytes.Length, 15));

                byte[] emailBytes = Encoding.ASCII.GetBytes(email ?? "");
                Array.Copy(emailBytes, 0, buffer, 54, Math.Min(emailBytes.Length, 40));

                try
                {
                    IncluirCliente(buffer);
                    string status = Encoding.ASCII.GetString(buffer, 94, 2).Trim();

                    // Distincao estrita de mensagens de erro baseada no novo dominio de status
                    if (status == "00")
                        Console.WriteLine("\n[SUCESSO] Novo cliente cadastrado com sucesso no sistema legado!");
                    else if (status == "02")
                        Console.WriteLine("\n[ALERTA] Falha no cadastro. O Código informado já se encontra atribuído a outro cliente (Status 02).");
                    else if (status == "03")
                        Console.WriteLine("\n[ERRO] Rejeição do motor legado: Nome vazio (Status 03).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FALHA CRÍTICA] Erro na rotina de inclusao nativa: {ex.Message}");
                }
            }

            PausarTela();
        }
    }

    static void ExibirBufferConsulta(byte[] buffer)
    {
        string status = Encoding.ASCII.GetString(buffer, 94, 2).Trim();
        if (status == "00")
        {
            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine("           DADOS DO CLIENTE LOCALIZADO              ");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Nome:     {Encoding.ASCII.GetString(buffer, 9, 30).TrimEnd()}");
            Console.WriteLine($"Telefone: {Encoding.ASCII.GetString(buffer, 39, 15).TrimEnd()}");
            Console.WriteLine($"E-mail:   {Encoding.ASCII.GetString(buffer, 54, 40).TrimEnd()}");
            Console.WriteLine("----------------------------------------------------");
        }
        else
        {
            Console.WriteLine("\n[ALERTA] Registro de cliente nao localizado (Status 01).");
        }
    }

    static void PausarTela()
    {
        Console.WriteLine("\nPressione qualquer tecla para retornar ao menu...");
        Console.ReadKey();
        Console.Clear();
    }
}