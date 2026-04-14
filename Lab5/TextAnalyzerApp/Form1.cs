using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextAnalyzerApp;

public partial class Form1 : Form
{
    private Label titleLabel = new();
    private Label infoLabel = new();
    private Label statusLabel = new();

    private Button btnAnalyzeFile = new();

    public Form1()
    {
        InitializeComponent();
        SetupUI();
    }

    private void SetupUI()
    {
        Text = "Програма підрахунку символів у файлі";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(700, 250);
        BackColor = Color.WhiteSmoke;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        titleLabel.Text = "Підрахунок кількості входжень кожного символу";
        titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        titleLabel.AutoSize = true;
        titleLabel.Left = 90;
        titleLabel.Top = 20;

        infoLabel.Text = "Оберіть текстовий файл, програма створить result.txt зі статистикою.";
        infoLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        infoLabel.AutoSize = true;
        infoLabel.Left = 70;
        infoLabel.Top = 70;

        btnAnalyzeFile.Text = "Аналіз файлу";
        btnAnalyzeFile.Left = 250;
        btnAnalyzeFile.Top = 120;
        btnAnalyzeFile.Width = 180;
        btnAnalyzeFile.Height = 40;
        btnAnalyzeFile.Click += AnalyzeFile_Click;

        statusLabel.Text = "Готово до роботи.";
        statusLabel.Left = 30;
        statusLabel.Top = 190;
        statusLabel.Width = 620;
        statusLabel.Height = 25;
        statusLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        statusLabel.ForeColor = Color.DarkSlateGray;

        Controls.Add(titleLabel);
        Controls.Add(infoLabel);
        Controls.Add(btnAnalyzeFile);
        Controls.Add(statusLabel);
    }

    private void AnalyzeFile_Click(object? sender, EventArgs e)
    {
        try
        {
            using OpenFileDialog dialog = new();
            dialog.Title = "Виберіть текстовий файл";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string text = File.ReadAllText(dialog.FileName);

                Dictionary<char, int> stats = new();

                foreach (char c in text)
                {
                    if (stats.ContainsKey(c))
                        stats[c]++;
                    else
                        stats[c] = 1;
                }

                int totalLetters = text.Count(char.IsLetter);

                string basePath = Path.Combine(Environment.CurrentDirectory, "Lab5Data");
                Directory.CreateDirectory(basePath);

                string resultPath = Path.Combine(basePath, "result.txt");

                List<string> lines = new();

                foreach (var pair in stats.OrderBy(p => p.Key))
                {
                    string symbolName = pair.Key switch
                    {
                        ' ' => "(space)",
                        '\n' => "(newline)",
                        '\r' => "(carriage return)",
                        '\t' => "(tab)",
                        _ => pair.Key.ToString()
                    };

                    lines.Add($"{symbolName} - {pair.Value}");
                }

                lines.Add("");
                lines.Add($"Загальна кількість букв: {totalLetters}");

                File.WriteAllLines(resultPath, lines);

                statusLabel.Text = $"Результат збережено у файл: {resultPath}";
                MessageBox.Show(
                    "Аналіз завершено успішно.",
                    "Успіх",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
