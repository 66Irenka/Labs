using System;
using System.IO;
using System.Windows.Forms;

namespace EncryptApp;

public partial class MainForm : Form
{
    private Label textFileLabel = new();
    private Label passwordFileLabel = new();
    private Label outputFileLabel = new();

    private TextBox textFilePathTextBox = new();
    private TextBox passwordFilePathTextBox = new();
    private TextBox outputFilePathTextBox = new();

    private Button selectTextFileButton = new();
    private Button selectPasswordFileButton = new();
    private Button selectOutputFileButton = new();
    private Button encryptButton = new();
    private Button generateFilesButton = new();

    private Label statusLabel = new();

    public MainForm()
    {
        InitializeComponent();
        SetupUi();
    }

    private void SetupUi()
    {
        Text = "Lab3 EncryptApp";
        ClientSize = new Size(760, 320);
        StartPosition = FormStartPosition.CenterScreen;

        textFileLabel.Text = "Файл тексту (Pavlenko1.txt):";
        textFileLabel.Left = 20;
        textFileLabel.Top = 20;
        textFileLabel.Width = 220;

        textFilePathTextBox.Left = 20;
        textFilePathTextBox.Top = 45;
        textFilePathTextBox.Width = 560;
        textFilePathTextBox.ReadOnly = true;

        selectTextFileButton.Text = "Обрати...";
        selectTextFileButton.Left = 600;
        selectTextFileButton.Top = 43;
        selectTextFileButton.Width = 120;
        selectTextFileButton.Click += SelectTextFileButton_Click;

        passwordFileLabel.Text = "Файл пароля (Pavlenko2.txt):";
        passwordFileLabel.Left = 20;
        passwordFileLabel.Top = 85;
        passwordFileLabel.Width = 220;

        passwordFilePathTextBox.Left = 20;
        passwordFilePathTextBox.Top = 110;
        passwordFilePathTextBox.Width = 560;
        passwordFilePathTextBox.ReadOnly = true;

        selectPasswordFileButton.Text = "Обрати...";
        selectPasswordFileButton.Left = 600;
        selectPasswordFileButton.Top = 108;
        selectPasswordFileButton.Width = 120;
        selectPasswordFileButton.Click += SelectPasswordFileButton_Click;

        outputFileLabel.Text = "Файл результату (Pavlenko3.txt):";
        outputFileLabel.Left = 20;
        outputFileLabel.Top = 150;
        outputFileLabel.Width = 230;

        outputFilePathTextBox.Left = 20;
        outputFilePathTextBox.Top = 175;
        outputFilePathTextBox.Width = 560;
        outputFilePathTextBox.ReadOnly = true;

        selectOutputFileButton.Text = "Зберегти як...";
        selectOutputFileButton.Left = 600;
        selectOutputFileButton.Top = 173;
        selectOutputFileButton.Width = 120;
        selectOutputFileButton.Click += SelectOutputFileButton_Click;

        encryptButton.Text = "Зашифрувати";
        encryptButton.Left = 20;
        encryptButton.Top = 225;
        encryptButton.Width = 160;
        encryptButton.Height = 35;
        encryptButton.Click += EncryptButton_Click;

        generateFilesButton.Text = "Створити файли";
        generateFilesButton.Left = 200;
        generateFilesButton.Top = 225;
        generateFilesButton.Width = 160;
        generateFilesButton.Height = 35;
        generateFilesButton.Click += GenerateFilesButton_Click;

        statusLabel.Left = 20;
        statusLabel.Top = 275;
        statusLabel.Width = 700;
        statusLabel.Height = 30;
        statusLabel.Text = "Оберіть 3 файли для шифрування.";

        Controls.Add(textFileLabel);
        Controls.Add(textFilePathTextBox);
        Controls.Add(selectTextFileButton);

        Controls.Add(passwordFileLabel);
        Controls.Add(passwordFilePathTextBox);
        Controls.Add(selectPasswordFileButton);

        Controls.Add(outputFileLabel);
        Controls.Add(outputFilePathTextBox);
        Controls.Add(selectOutputFileButton);

        Controls.Add(encryptButton);
        Controls.Add(generateFilesButton);
        Controls.Add(statusLabel);
    }

    private void SelectTextFileButton_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new();
        dialog.Title = "Оберіть файл тексту";
        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            textFilePathTextBox.Text = dialog.FileName;
        }
    }

    private void SelectPasswordFileButton_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new();
        dialog.Title = "Оберіть файл пароля";
        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            passwordFilePathTextBox.Text = dialog.FileName;
        }
    }

    private void SelectOutputFileButton_Click(object? sender, EventArgs e)
    {
        using SaveFileDialog dialog = new();
        dialog.Title = "Оберіть файл для збереження шифру";
        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        dialog.FileName = "Pavlenko3.txt";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            outputFilePathTextBox.Text = dialog.FileName;
        }
    }

    private void GenerateFilesButton_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Environment.CurrentDirectory;
            string file1 = Path.Combine(basePath, "Pavlenko1.txt");
            string file2 = Path.Combine(basePath, "Pavlenko2.txt");

            string text = "життяцецікавийпроцеслюдинапостійновчитьсярозвиваєтьсяпомиляєтьсятазновупробуєкоженденьдаєновіможливостіважливонебоятисьзмініризикувативіритисебетасвоїсиликолимипрацюємонидособоюмистаємосильнішимивпевненішимитащасливішимисправжнійуспіхприходитьдотиххтонезупиняєтьсяпередтруднощамиатакожвмієцінуватималіречіпростітамаленькімоментирадостівнашомужиттіможутьзробитивеликийвпливважливопамятатипрощосамежиттяоднеітребапрожитийогогіднозлюбовютасенсом";
            string password = "секретнийпароль";

            File.WriteAllText(file1, text);
            File.WriteAllText(file2, password);

            textFilePathTextBox.Text = file1;
            passwordFilePathTextBox.Text = file2;
            statusLabel.Text = "Файли Pavlenko1.txt і Pavlenko2.txt створено.";
            MessageBox.Show("Файли створені успішно.", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка: {ex.Message}", "Помилка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void EncryptButton_Click(object? sender, EventArgs e)
    {
        try
        {
            string textPath = textFilePathTextBox.Text;
            string passwordPath = passwordFilePathTextBox.Text;
            string outputPath = outputFilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(textPath) ||
                string.IsNullOrWhiteSpace(passwordPath) ||
                string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("Оберіть усі потрібні файли.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string text = File.ReadAllText(textPath).Trim();
            string password = File.ReadAllText(passwordPath).Trim();

            if (text.Length < 500)
            {
                MessageBox.Show("Текст у Pavlenko1.txt має бути не менше 500 символів.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 10)
            {
                MessageBox.Show("Пароль у Pavlenko2.txt має бути не менше 10 символів.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (char c in text)
{
    if (!SymbolTable.IsSupported(c) && c != ' ' && c != '.' && c != ',' && c != '!' && c != '\n' && c != '\r')
    {
        MessageBox.Show($"Символ '{c}' у тексті не підтримується таблицею.", "Помилка",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
}

            foreach (char c in password)
            {
                if (!SymbolTable.IsSupported(c))
                {
                    MessageBox.Show($"Символ '{c}' у паролі не підтримується таблицею.", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string encryptedText = CipherService.Encrypt(text, password);
            File.WriteAllText(outputPath, encryptedText);

            statusLabel.Text = $"Готово. Зашифрований текст збережено у: {outputPath}";
            MessageBox.Show("Шифрування завершено успішно.", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}