using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjetoFinal.Core
{
    public class CobolService
    {
        // Assinaturas nativas encapsuladas para seguranca de escopo
        [DllImport("libcob-4", EntryPoint = "cob_init", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CobInit(int argc, IntPtr argv);

        [DllImport("consulta.dll", EntryPoint = "consulta", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ConsultarClienteNative(byte[] buffer);

        [DllImport("alteracao.dll", EntryPoint = "alteracao", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AlterarClienteNative(byte[] buffer);

        [DllImport("exclusao.dll", EntryPoint = "exclusao", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ExcluirClienteNative(byte[] buffer);

        [DllImport("inclusao.dll", EntryPoint = "inclusao", CallingConvention = CallingConvention.Cdecl)]
        private static extern void IncluirClienteNative(byte[] buffer);

        private static bool _isInitialized = false;

        public static void InicializarRuntime()
        {
            if (_isInitialized) return;

            string msysBinPath = @"C:\msys64\mingw64\bin";
            if (Directory.Exists(msysBinPath))
            {
                string currentPath = Environment.GetEnvironmentVariable("PATH") ?? "";
                Environment.SetEnvironmentVariable("PATH", msysBinPath + Path.PathSeparator + currentPath);
            }

            try
            {
                CobInit(0, IntPtr.Zero);
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Falha ao acionar a engine do GnuCOBOL: {ex.Message}", ex);
            }
        }

        public static ClienteModel Consultar(string codigo)
        {
            InicializarRuntime();
            byte[] buffer = CriarBufferBase(codigo);

            ConsultarClienteNative(buffer);
            string status = Encoding.ASCII.GetString(buffer, 94, 2).Trim();

            if (status == "00")
            {
                return new ClienteModel
                {
                    Codigo = codigo,
                    Nome = Encoding.ASCII.GetString(buffer, 9, 30).TrimEnd(),
                    Telefone = Encoding.ASCII.GetString(buffer, 39, 15).TrimEnd(),
                    Email = Encoding.ASCII.GetString(buffer, 54, 40).TrimEnd(),
                    StatusCode = status
                };
            }

            return new ClienteModel { StatusCode = status };
        }

        public static string Alterar(string codigo, string telefone, string email)
        {
            InicializarRuntime();
            byte[] buffer = CriarBufferBase(codigo);

            // Higienizacao posicional defensiva das fatias mutaveis do buffer
            for (int i = 39; i < 94; i++) buffer[i] = (byte)' ';

            byte[] telBytes = Encoding.ASCII.GetBytes(telefone ?? "");
            Array.Copy(telBytes, 0, buffer, 39, Math.Min(telBytes.Length, 15));

            byte[] emailBytes = Encoding.ASCII.GetBytes(email ?? "");
            Array.Copy(emailBytes, 0, buffer, 54, Math.Min(emailBytes.Length, 40));

            AlterarClienteNative(buffer);
            return Encoding.ASCII.GetString(buffer, 94, 2).Trim();
        }

        public static string Excluir(string codigo)
        {
            InicializarRuntime();
            byte[] buffer = CriarBufferBase(codigo);

            ExcluirClienteNative(buffer);
            return Encoding.ASCII.GetString(buffer, 94, 2).Trim();
        }

        public static string Incluir(string codigo, string nome, string telefone, string email)
        {
            InicializarRuntime();
            byte[] buffer = CriarBufferBase(codigo);

            for (int i = 9; i < 94; i++) buffer[i] = (byte)' ';

            byte[] nomeBytes = Encoding.ASCII.GetBytes(nome ?? "");
            Array.Copy(nomeBytes, 0, buffer, 9, Math.Min(nomeBytes.Length, 30));

            byte[] telBytes = Encoding.ASCII.GetBytes(telefone ?? "");
            Array.Copy(telBytes, 0, buffer, 39, Math.Min(telBytes.Length, 15));

            byte[] emailBytes = Encoding.ASCII.GetBytes(email ?? "");
            Array.Copy(emailBytes, 0, buffer, 54, Math.Min(emailBytes.Length, 40));

            IncluirClienteNative(buffer);
            return Encoding.ASCII.GetString(buffer, 94, 2).Trim();
        }

        private static byte[] CriarBufferBase(string codigo)
        {
            byte[] buffer = new byte[96];
            Array.Fill(buffer, (byte)' ');
            byte[] codigoBytes = Encoding.ASCII.GetBytes(codigo ?? "");
            Array.Copy(codigoBytes, 0, buffer, 0, Math.Min(codigoBytes.Length, 9));
            return buffer;
        }
    }

    public class ClienteModel
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string StatusCode { get; set; }
    }
}