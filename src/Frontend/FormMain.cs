using System;
using System.Drawing;
using System.Windows.Forms;
using ProjetoFinal.Core;

public class FormMain : Form
{
    private TabControl tabControl;
    private TabPage tabConsultaExclusao;
    private TabPage tabCadastroAlteracao;

    // Controles da Aba 1: Consulta/Exclusao
    private TextBox txtCodigo;
    private Button btnConsultar;
    private Button btnExcluir;
    private Label lblNomeValor;
    private Label lblTelefoneValor;
    private Label lblEmailValor;
    private GroupBox groupResultados;

    // Controles da Aba 2: Cadastro
    private TextBox txtCadCodigo;
    private TextBox txtCadNome;
    private TextBox txtCadTelefone;
    private TextBox txtCadEmail;
    private Button btnCadastrar;

    // Controles da Aba 2: Alteracao
    private TextBox txtAltCodigo;
    private TextBox txtAltNome;
    private TextBox txtAltTelefone;
    private TextBox txtAltEmail;
    private Button btnAltCarregar;
    private Button btnAltSalvar;

    public FormMain()
    {
        this.Text = "COOPERATIVA FINANCEIRA ALFA - INTEGRADA INTEGRAÇÃO DE SISTEMAS";
        this.Size = new Size(720, 520);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        tabControl = new TabControl { Dock = DockStyle.Fill };
        tabConsultaExclusao = new TabPage { Text = " Consulta & Exclusão " };
        tabCadastroAlteracao = new TabPage { Text = " Cadastro & Alteração " };

        ConfigurarAbaConsultaExclusao();
        ConfigurarAbaCadastroAlteracao();

        tabControl.TabPages.Add(tabConsultaExclusao);
        tabControl.TabPages.Add(tabCadastroAlteracao);
        this.Controls.Add(tabControl);
    }

    private void ConfigurarAbaConsultaExclusao()
    {
        Label lblInstrucao = new Label { Text = "Digite o Código do Cliente (9 dígitos):", Location = new Point(20, 25), Size = new Size(250, 20), Font = new Font(Label.DefaultFont, FontStyle.Bold) };
        txtCodigo = new TextBox { Location = new Point(20, 48), Size = new Size(180, 25), MaxLength = 9 };
        txtCodigo.TextChanged += (s, e) => { btnConsultar.Enabled = (txtCodigo.Text.Trim().Length == 9 && long.TryParse(txtCodigo.Text, out _)); };

        btnConsultar = new Button { Text = "Buscar Cliente", Location = new Point(210, 46), Size = new Size(120, 28), Enabled = false };
        btnConsultar.Click += BtnConsultar_Click;

        groupResultados = new GroupBox { Text = " Ficha Cadastral do Cooperado ", Location = new Point(20, 100), Size = new Size(660, 220), Visible = false };

        Label lblNome = new Label { Text = "Nome do Cliente:", Location = new Point(20, 35), Size = new Size(120, 20), ForeColor = Color.DimGray };
        lblNomeValor = new Label { Location = new Point(150, 35), Size = new Size(480, 20), Font = new Font(Label.DefaultFont, FontStyle.Bold) };

        Label lblTelefone = new Label { Text = "Telefone:", Location = new Point(20, 75), Size = new Size(120, 20), ForeColor = Color.DimGray };
        lblTelefoneValor = new Label { Location = new Point(150, 75), Size = new Size(480, 20), Font = new Font(Label.DefaultFont, FontStyle.Bold) };

        Label lblEmail = new Label { Text = "E-mail:", Location = new Point(20, 115), Size = new Size(120, 20), ForeColor = Color.DimGray };
        lblEmailValor = new Label { Location = new Point(150, 115), Size = new Size(480, 20), Font = new Font(Label.DefaultFont, FontStyle.Bold) };

        btnExcluir = new Button { Text = "Excluir Cliente Definitivamente", Location = new Point(20, 165), Size = new Size(220, 35), BackColor = Color.Firebrick, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnExcluir.Click += BtnExcluir_Click;

        groupResultados.Controls.AddRange(new Control[] { lblNome, lblNomeValor, lblTelefone, lblTelefoneValor, lblEmail, lblEmailValor, btnExcluir });
        tabConsultaExclusao.Controls.AddRange(new Control[] { lblInstrucao, txtCodigo, btnConsultar, groupResultados });
    }

    private void ConfigurarAbaCadastroAlteracao()
    {
        // --- FORMULÁRIO DE NOVO CADASTRO (LADO ESQUERDO) ---
        GroupBox groupCadastro = new GroupBox { Text = " Incluir Novo Cooperado ", Location = new Point(15, 15), Size = new Size(330, 420) };
        
        Label lblCadCod = new Label { Text = "Código (9 dígitos):", Location = new Point(15, 30), Size = new Size(150, 18) };
        txtCadCodigo = new TextBox { Location = new Point(15, 50), Size = new Size(140, 23), MaxLength = 9 };
        
        Label lblCadNome = new Label { Text = "Nome Completo (Obrigatório):", Location = new Point(15, 90), Size = new Size(200, 18) };
        txtCadNome = new TextBox { Location = new Point(15, 110), Size = new Size(290, 23), MaxLength = 30 };

        Label lblCadTel = new Label { Text = "Telefone:", Location = new Point(15, 160), Size = new Size(150, 18) };
        txtCadTelefone = new TextBox { Location = new Point(15, 180), Size = new Size(290, 23), MaxLength = 15 };

        Label lblCadEmail = new Label { Text = "E-mail:", Location = new Point(15, 230), Size = new Size(150, 18) };
        txtCadEmail = new TextBox { Location = new Point(15, 250), Size = new Size(290, 23), MaxLength = 40 };

        btnCadastrar = new Button { Text = "Persistir Novo Cadastro", Location = new Point(15, 310), Size = new Size(290, 35), BackColor = Color.ForestGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font(Label.DefaultFont, FontStyle.Bold) };
        btnCadastrar.Click += BtnCadastrar_Click;

        groupCadastro.Controls.AddRange(new Control[] { lblCadCod, txtCadCodigo, lblCadNome, txtCadNome, lblCadTel, txtCadTelefone, lblCadEmail, txtCadEmail, btnCadastrar });

        // --- FORMULÁRIO DE ALTERAÇÃO CADASTRAL (LADO DIREITO) ---
        GroupBox groupAlteracao = new GroupBox { Text = " Modificar Informações de Contato ", Location = new Point(360, 15), Size = new Size(330, 420) };

        Label lblAltCod = new Label { Text = "Código do Cooperado:", Location = new Point(15, 30), Size = new Size(150, 18) };
        txtAltCodigo = new TextBox { Location = new Point(15, 50), Size = new Size(140, 23), MaxLength = 9 };

        btnAltCarregar = new Button { Text = "Carregar Dados", Location = new Point(165, 48), Size = new Size(140, 26) };
        btnAltCarregar.Click += BtnAltCarregar_Click;

        Label lblAltNome = new Label { Text = "Nome (Bloqueado para Edição):", Location = new Point(15, 90), Size = new Size(250, 18) };
        txtAltNome = new TextBox { Location = new Point(15, 110), Size = new Size(290, 23), ReadOnly = true, BackColor = Color.LightGray };

        Label lblAltTel = new Label { Text = "Novo Telefone:", Location = new Point(15, 160), Size = new Size(150, 18) };
        txtAltTelefone = new TextBox { Location = new Point(15, 180), Size = new Size(290, 23), MaxLength = 15, Enabled = false };

        Label lblAltEmail = new Label { Text = "Novo E-mail:", Location = new Point(15, 230), Size = new Size(150, 18) };
        txtAltEmail = new TextBox { Location = new Point(15, 250), Size = new Size(290, 23), MaxLength = 40, Enabled = false };

        btnAltSalvar = new Button { Text = "Salvar Alterações de Contato", Location = new Point(15, 310), Size = new Size(290, 35), BackColor = Color.SteelBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false, Font = new Font(Label.DefaultFont, FontStyle.Bold) };
        btnAltSalvar.Click += BtnAltSalvar_Click;

        groupAlteracao.Controls.AddRange(new Control[] { lblAltCod, txtAltCodigo, btnAltCarregar, lblAltNome, txtAltNome, lblAltTel, txtAltTelefone, lblAltEmail, txtAltEmail, btnAltSalvar });

        // Adiciona ambos os painéis na segunda aba
        tabCadastroAlteracao.Controls.AddRange(new Control[] { groupCadastro, groupAlteracao });
    }

    // --- MANIPULADORES DE EVENTOS DA ABA 1 ---
    private void BtnConsultar_Click(object sender, EventArgs e)
    {
        var cliente = CobolService.Consultar(txtCodigo.Text.Trim());
        if (cliente.StatusCode == "00")
        {
            lblNomeValor.Text = cliente.Nome;
            lblTelefoneValor.Text = cliente.Telefone;
            lblEmailValor.Text = cliente.Email;
            groupResultados.Visible = true;
        }
        else
        {
            groupResultados.Visible = false;
            MessageBox.Show("Cooperado não localizado na base cadastral.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnExcluir_Click(object sender, EventArgs e)
    {
        DialogResult res = MessageBox.Show($"Confirma a EXCLUSÃO definitiva do cooperado {lblNomeValor.Text}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        if (res != DialogResult.Yes) return;

        if (CobolService.Excluir(txtCodigo.Text.Trim()) == "00")
        {
            MessageBox.Show("Cliente removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            groupResultados.Visible = false;
            txtCodigo.Clear();
        }
    }

    // --- MANIPULADORES DE EVENTOS DA ABA 2 (CADASTRO) ---
    private void BtnCadastrar_Click(object sender, EventArgs e)
    {
        string cod = txtCadCodigo.Text.Trim();
        string nome = txtCadNome.Text.Trim();
        string tel = txtCadTelefone.Text.Trim();
        string email = txtCadEmail.Text.Trim();

        if (cod.Length != 9 || !long.TryParse(cod, out _))
        {
            MessageBox.Show("O código deve conter exatamente 9 dígitos numéricos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (string.IsNullOrEmpty(nome))
        {
            MessageBox.Show("O Nome do cliente é um campo obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string status = CobolService.Incluir(cod, nome, tel, email);
        if (status == "00")
        {
            MessageBox.Show("Novo cliente cadastrado com sucesso no sistema!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtCadCodigo.Clear(); txtCadNome.Clear(); txtCadTelefone.Clear(); txtCadEmail.Clear();
        }
        else if (status == "02")
        {
            MessageBox.Show("Operação rejeitada. O código informado já se encontra atribuído a outro cliente.", "Duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            MessageBox.Show($"O motor legado recusou a operação (Status {status}).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // --- MANIPULADORES DE EVENTOS DA ABA 2 (ALTERAÇÃO) ---
    private void BtnAltCarregar_Click(object sender, EventArgs e)
    {
        string cod = txtAltCodigo.Text.Trim();
        if (cod.Length != 9 || !long.TryParse(cod, out _))
        {
            MessageBox.Show("Digite um código válido de 9 dígitos para carregar.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var cliente = CobolService.Consultar(cod);
        if (cliente.StatusCode == "00")
        {
            txtAltNome.Text = cliente.Nome;
            txtAltTelefone.Text = cliente.Telefone;
            txtAltEmail.Text = cliente.Email;
            
            // Habilita as fatias de edicao de contato
            txtAltTelefone.Enabled = true;
            txtAltEmail.Enabled = true;
            btnAltSalvar.Enabled = true;
        }
        else
        {
            txtAltNome.Clear(); txtAltTelefone.Clear(); txtAltEmail.Clear();
            txtAltTelefone.Enabled = false; txtAltEmail.Enabled = false; btnAltSalvar.Enabled = false;
            MessageBox.Show("Cliente não localizado para alteração.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnAltSalvar_Click(object sender, EventArgs e)
    {
        string cod = txtAltCodigo.Text.Trim();
        string tel = txtAltTelefone.Text.Trim();
        string email = txtAltEmail.Text.Trim();

        if (string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(email))
        {
            MessageBox.Show("Os campos de Telefone e E-mail não podem ser limpos por completo.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string status = CobolService.Alterar(cod, tel, email);
        if (status == "00")
        {
            MessageBox.Show("Contato atualizado com sucesso no sistema!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtAltCodigo.Clear(); txtAltNome.Clear(); txtAltTelefone.Clear(); txtAltEmail.Clear();
            txtAltTelefone.Enabled = false; txtAltEmail.Enabled = false; btnAltSalvar.Enabled = false;
        }
        else
        {
            MessageBox.Show($"Falha ao persistir alterações (Status {status}).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}