using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuestionsApp;

public partial class Form1 : Form
{
    private Label titleLabel = new();
    private Label questionsTitleLabel = new();
    private Label answersTitleLabel = new();
    private Label statusLabel = new();

    private readonly TextBox[] questionBoxes = new TextBox[5];
    private readonly TextBox[] answerBoxes = new TextBox[5];

    private Button btnGenerateFiles = new();
    private Button btnSaveQuestions = new();
    private Button btnOpenQuestions = new();
    private Button btnSaveAnswers = new();
    private Button btnOpenAnswers = new();

    public Form1()
    {
        InitializeComponent();
        SetupUI();
    }

    private void SetupUI()
    {
        Text = "Програма для передачі запитань колегам з групи";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(980, 430);
        BackColor = Color.WhiteSmoke;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        titleLabel.Text = "Програма для передачі запитань колегам з групи";
        titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        titleLabel.AutoSize = true;
        titleLabel.Left = 220;
        titleLabel.Top = 20;

        questionsTitleLabel.Text = "Питання";
        questionsTitleLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        questionsTitleLabel.AutoSize = true;
        questionsTitleLabel.Left = 170;
        questionsTitleLabel.Top = 70;

        answersTitleLabel.Text = "Відповіді";
        answersTitleLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        answersTitleLabel.AutoSize = true;
        answersTitleLabel.Left = 640;
        answersTitleLabel.Top = 70;

        Controls.Add(titleLabel);
        Controls.Add(questionsTitleLabel);
        Controls.Add(answersTitleLabel);

        for (int i = 0; i < 5; i++)
        {
            Label qLabel = new();
            qLabel.Text = $"Питання {i + 1}";
            qLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            qLabel.Left = 40;
            qLabel.Top = 110 + i * 45;
            qLabel.Width = 90;

            TextBox qBox = new();
            qBox.Left = 130;
            qBox.Top = 106 + i * 45;
            qBox.Width = 280;
            qBox.Height = 27;
            qBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            questionBoxes[i] = qBox;

            Label aLabel = new();
            aLabel.Text = $"Відповідь {i + 1}";
            aLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            aLabel.Left = 500;
            aLabel.Top = 110 + i * 45;
            aLabel.Width = 100;

            TextBox aBox = new();
            aBox.Left = 610;
            aBox.Top = 106 + i * 45;
            aBox.Width = 280;
            aBox.Height = 27;
            aBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            answerBoxes[i] = aBox;

            Controls.Add(qLabel);
            Controls.Add(qBox);
            Controls.Add(aLabel);
            Controls.Add(aBox);
        }

        btnGenerateFiles.Text = "Створити файли";
        btnGenerateFiles.Left = 40;
        btnGenerateFiles.Top = 345;
        btnGenerateFiles.Width = 150;
        btnGenerateFiles.Height = 35;
        btnGenerateFiles.Click += GenerateFiles_Click;

        btnSaveQuestions.Text = "Зберегти питання";
        btnSaveQuestions.Left = 210;
        btnSaveQuestions.Top = 345;
        btnSaveQuestions.Width = 150;
        btnSaveQuestions.Height = 35;
        btnSaveQuestions.Click += SaveQuestions_Click;

        btnOpenQuestions.Text = "Відкрити питання";
        btnOpenQuestions.Left = 380;
        btnOpenQuestions.Top = 345;
        btnOpenQuestions.Width = 150;
        btnOpenQuestions.Height = 35;
        btnOpenQuestions.Click += OpenQuestions_Click;

        btnSaveAnswers.Text = "Зберегти відповіді";
        btnSaveAnswers.Left = 550;
        btnSaveAnswers.Top = 345;
        btnSaveAnswers.Width = 150;
        btnSaveAnswers.Height = 35;
        btnSaveAnswers.Click += SaveAnswers_Click;

        btnOpenAnswers.Text = "Відкрити відповіді";
        btnOpenAnswers.Left = 720;
        btnOpenAnswers.Top = 345;
        btnOpenAnswers.Width = 150;
        btnOpenAnswers.Height = 35;
        btnOpenAnswers.Click += OpenAnswers_Click;

        statusLabel.Text = "Готово до роботи.";
        statusLabel.Left = 40;
        statusLabel.Top = 390;
        statusLabel.Width = 850;
        statusLabel.Height = 25;
        statusLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        statusLabel.ForeColor = Color.DarkSlateGray;

        Controls.Add(btnGenerateFiles);
        Controls.Add(btnSaveQuestions);
        Controls.Add(btnOpenQuestions);
        Controls.Add(btnSaveAnswers);
        Controls.Add(btnOpenAnswers);
        Controls.Add(statusLabel);
    }

    private void GenerateFiles_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Lab4Data");
            Directory.CreateDirectory(basePath);

            string questionsPath = Path.Combine(basePath, "Pavlenko1.txt");
            string answersPath = Path.Combine(basePath, "Pavlenko2.txt");

            FileService.CreateEmptyFile(questionsPath);
            FileService.CreateEmptyFile(answersPath);

            List<string> defaultQuestions = new()
            {
                "Як тебе звати?",
                "Яка твоя улюблена мова програмування?",
                "Чому ти обрав(ла) навчання в цій групі?",
                "Який предмет тобі найбільше подобається?",
                "Які у тебе плани після навчання?"
            };

            FileService.WriteLines(questionsPath, defaultQuestions);
            FileService.WriteLines(answersPath, new List<string> { "", "", "", "", "" });

            for (int i = 0; i < 5; i++)
            {
                questionBoxes[i].Text = defaultQuestions[i];
                answerBoxes[i].Text = "";
            }

            statusLabel.Text = $"Файли створено у папці: {basePath}";

            MessageBox.Show(
                "Файли Pavlenko1.txt і Pavlenko2.txt створено у папці Lab4Data.",
                "Успіх",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка при створенні файлів: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SaveQuestions_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Lab4Data");
            Directory.CreateDirectory(basePath);

            using SaveFileDialog dialog = new();
            dialog.Title = "Зберегти файл із питаннями";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = basePath;
            dialog.FileName = "Pavlenko1.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<string> questions = questionBoxes.Select(q => q.Text).ToList();
                FileService.WriteLines(dialog.FileName, questions);
                statusLabel.Text = $"Питання збережено у файл: {dialog.FileName}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка при збереженні питань: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void OpenQuestions_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Lab4Data");
            Directory.CreateDirectory(basePath);

            using OpenFileDialog dialog = new();
            dialog.Title = "Відкрити файл із питаннями";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FileName = "Pavlenko1.txt";
            dialog.InitialDirectory = basePath;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<string> questions = FileService.ReadLines(dialog.FileName);

                for (int i = 0; i < questionBoxes.Length; i++)
                {
                    questionBoxes[i].Clear();
                }

                for (int i = 0; i < questions.Count && i < questionBoxes.Length; i++)
                {
                    questionBoxes[i].Text = questions[i];
                }

                statusLabel.Text = $"Питання відкрито з файлу: {dialog.FileName}";
                MessageBox.Show(
                    "Питання успішно завантажено.",
                    "Успіх",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка при відкритті питань: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void SaveAnswers_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Lab4Data");
            Directory.CreateDirectory(basePath);

            using SaveFileDialog dialog = new();
            dialog.Title = "Зберегти файл із відповідями";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = basePath;
            dialog.FileName = "Pavlenko2.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<string> answers = answerBoxes.Select(a => a.Text).ToList();
                FileService.WriteLines(dialog.FileName, answers);
                statusLabel.Text = $"Відповіді збережено у файл: {dialog.FileName}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка при збереженні відповідей: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void OpenAnswers_Click(object? sender, EventArgs e)
    {
        try
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "Lab4Data");
            Directory.CreateDirectory(basePath);

            using OpenFileDialog dialog = new();
            dialog.Title = "Відкрити файл із відповідями";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FileName = "Pavlenko2.txt";
            dialog.InitialDirectory = basePath;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<string> answers = FileService.ReadLines(dialog.FileName);

                for (int i = 0; i < answerBoxes.Length; i++)
                {
                    answerBoxes[i].Clear();
                }

                for (int i = 0; i < answers.Count && i < answerBoxes.Length; i++)
                {
                    answerBoxes[i].Text = answers[i];
                }

                statusLabel.Text = $"Відповіді відкрито з файлу: {dialog.FileName}";
                MessageBox.Show(
                    "Відповіді успішно завантажено.",
                    "Успіх",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Помилка при відкритті відповідей: {ex.Message}",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}