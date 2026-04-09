using System;
using System.IO;
using System.Windows.Forms;

namespace DecryptApp;

public partial class MainForm : Form
{
    private Label passwordFileLabel = new();
    private Label encryptedFileLabel = new();
    private Label outputFileLabel = new();

    private TextBox passwordFilePathTextBox = new();
    private TextBox encryptedFilePathTextBox = new();
    private TextBox outputFilePathTextBox = new();

    private Button selectPasswordFileButton = new();
    private Button selectEncryptedFileButton = new();
    private Button selectOutputFileButton = new();
    private Button decryptButton = new();

    private Label statusLabel = new();

    public MainForm()
    {
        InitializeComponent();
        SetupUi();
    }

    private void SetupUi()
    {
        Text = "Lab3 DecryptApp";
        ClientSize = new Size(760, 320);
        StartPosition = FormStartPosition.CenterScreen;

        passwordFileLabel.Text = "Файл пароля (Pavlenko2.txt):";
        passwordFileLabel.Left = 20;
        passwordFileLabel.Top = 20;
        passwordFileLabel.Width = 220;

        passwordFilePathTextBox.Left = 20;
        passwordFilePathTextBox.Top = 45;
        passwordFilePathTextBox.Width = 560;
        passwordFilePathTextBox.ReadOnly = true;

        selectPasswordFileButton.Text = "Обрати...";
        selectPasswordFileButton.Left = 600;
        selectPasswordFileButton.Top = 43;
        selectPasswordFileButton.Width = 120;
        selectPasswordFileButton.Click += SelectPasswordFileButton_Click;

        encryptedFileLabel.Text = "Файл шифру (Pavlenko3.txt):";
        encryptedFileLabel.Left = 20;
        encryptedFileLabel.Top = 85;
        encryptedFileLabel.Width = 220;

        encryptedFilePathTextBox.Left = 20;
        encryptedFilePathTextBox.Top = 110;
        encryptedFilePathTextBox.Width = 560;
        encryptedFilePathTextBox.ReadOnly = true;

        selectEncryptedFileButton.Text = "Обрати...";
        selectEncryptedFileButton.Left = 600;
        selectEncryptedFileButton.Top = 108;
        selectEncryptedFileButton.Width = 120;
        selectEncryptedFileButton.Click += SelectEncryptedFileButton_Click;

        outputFileLabel.Text = "Файл результату (Pavlenko4.txt):";
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

        decryptButton.Text = "Розшифрувати";
        decryptButton.Left = 20;
        decryptButton.Top = 225;
        decryptButton.Width = 160;
        decryptButton.Height = 35;
        decryptButton.Click += DecryptButton_Click;

        statusLabel.Left = 20;
        statusLabel.Top = 275;
        statusLabel.Width = 700;
        statusLabel.Height = 30;
        statusLabel.Text = "Оберіть 3 файли для розшифрування.";

        Controls.Add(passwordFileLabel);
        Controls.Add(passwordFilePathTextBox);
        Controls.Add(selectPasswordFileButton);

        Controls.Add(encryptedFileLabel);
        Controls.Add(encryptedFilePathTextBox);
        Controls.Add(selectEncryptedFileButton);

        Controls.Add(outputFileLabel);
        Controls.Add(outputFilePathTextBox);
        Controls.Add(selectOutputFileButton);

        Controls.Add(decryptButton);
        Controls.Add(statusLabel);
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

    private void SelectEncryptedFileButton_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new();
        dialog.Title = "Оберіть файл шифру";
        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            encryptedFilePathTextBox.Text = dialog.FileName;
        }
    }

    private void SelectOutputFileButton_Click(object? sender, EventArgs e)
    {
        using SaveFileDialog dialog = new();
        dialog.Title = "Оберіть файл для збереження результату";
        dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        dialog.FileName = "Pavlenko4.txt";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            outputFilePathTextBox.Text = dialog.FileName;
        }
    }

    private void DecryptButton_Click(object? sender, EventArgs e)
    {
        try
        {
            string passwordPath = passwordFilePathTextBox.Text;
            string encryptedPath = encryptedFilePathTextBox.Text;
            string outputPath = outputFilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(passwordPath) ||
                string.IsNullOrWhiteSpace(encryptedPath) ||
                string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("Оберіть усі потрібні файли.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string password = File.ReadAllText(passwordPath).Trim();
            string encryptedText = File.ReadAllText(encryptedPath).Trim();

            if (password.Length < 10)
            {
                MessageBox.Show("Пароль має бути не менше 10 символів.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

            foreach (char c in encryptedText)
            {
                 if (!SymbolTable.IsSupported(c) && c != ' ' && c != '.' && c != ',' && c != '!' && c != '\n' && c != '\r')
                {
                    MessageBox.Show($"Символ '{c}' у зашифрованому тексті не підтримується таблицею.", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string decryptedText = CipherService.Decrypt(encryptedText, password);
            File.WriteAllText(outputPath, decryptedText);

            statusLabel.Text = $"Готово. Результат збережено у: {outputPath}";
            MessageBox.Show("Розшифрування завершено успішно.", "Успіх",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}