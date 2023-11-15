using QuizWinform.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizWinform
{
    public partial class Quiz : Form
    {
        private List<Question> questions; // Add this variable to store the loaded questions
        private int currentQuestionIndex = 0; // Add this variable to keep track of the current question index
        private PRN_ProjectContext context;
        private Dictionary<int, Dictionary<string, bool>> checkboxStates = new Dictionary<int, Dictionary<string, bool>>();
        private Dictionary<int, Button> questionButtons = new Dictionary<int, Button>();

        public Quiz()
        {
            InitializeComponent();
            context = new PRN_ProjectContext();
        }
        public Quiz(string examCode, string studentName)
        {
            InitializeComponent();

            // Set labels with the values from Form1
            lbExamCode.Text = examCode;
            lbStudent.Text = studentName;
            context = new PRN_ProjectContext();
        }
        private void LoadQuestionByExamCode(string examCode)
        {
            try
            {
                // Assuming you have a DbSet property named "Exams" in your DbContext
                var exam = context.Exams
                    .Where(e => e.ExamName == examCode)
                    .FirstOrDefault();

                if (exam != null)
                {
                    // Get the ExamId for the specified ExamCode
                    int examId = exam.ExamId;

                    // Assuming you have a DbSet property named "Questions" in your DbContext
                    var question = context.Questions
                        .Where(q => q.ExamId == examId)
                        .FirstOrDefault();

                    if (question != null)
                    {
                        // Clear existing controls in the group box
                        gbQuestion.Controls.Clear();

                        // Create a Label to display the concatenated string with larger font size and line breaks
                        Label lblQuestion = new Label();
                        lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 16
                        lblQuestion.Text = $"Question 1: {question.QuestionText}\n" +
                                           $"A: {question.OptionA}\n" +
                                           $"B: {question.OptionB}\n" +
                                           $"C: {question.OptionC}\n" +
                                           $"D: {question.OptionD}";
                        lblQuestion.Top = 20;
                        lblQuestion.Width = gbQuestion.Width - 40; // Adjust width based on your design
                        lblQuestion.Height = 200; // Adjust height based on your design
                        lblQuestion.AutoSize = false; // Ensure the label size is fixed

                        // Add the Label to the group box
                        gbQuestion.Controls.Add(lblQuestion);
                    }
                    else
                    {
                        MessageBox.Show("No questions found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Exam not found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error loading question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateQuestionButtonsForExam(int examId)
        {
            try
            {
                // Assuming you have a DbSet property named "Questions" in your DbContext
                questions = context.Questions
                    .Where(q => q.ExamId == examId)
                    .ToList();

                if (questions.Any())
                {
                    // Clear existing controls outside the group box
                    ClearAllCheckboxes();

                    int buttonTop = 660;
                    int buttonLeft = 152;
                    int buttonWidth = 28;
                    int buttonHeight = 27;
                    int horizontalSpacing = 15;
                    int buttonNumber = 1;

                    foreach (var question in questions)
                    {
                        int currentButtonNumber = buttonNumber;

                        Button btnQuest = new Button();
                        btnQuest.Location = new Point(buttonLeft, buttonTop);
                        btnQuest.Size = new Size(buttonWidth, buttonHeight);
                        btnQuest.Text = currentButtonNumber.ToString();
                        btnQuest.Name = $"btnQuest{question.QuestionId}";

                        // Add the button to the dictionary
                        questionButtons[currentButtonNumber - 1] = btnQuest;

                        btnQuest.Click += (sender, e) =>
                        {
                            // Save the checkbox states for the current question
                            SaveCheckboxStates(questions[currentQuestionIndex], currentQuestionIndex);

                            // Check if any checkbox is checked for the current question
                            bool isAnyCheckboxChecked = checkboxStates[currentQuestionIndex].Any(kv => kv.Value);

                            // If any checkbox is checked, turn the corresponding button green
                            if (isAnyCheckboxChecked)
                            {
                                Button currentButton = questionButtons[currentQuestionIndex];
                                if (currentButton != null)
                                {
                                    currentButton.BackColor = Color.Green;
                                }
                            }

                            ClearAllCheckboxes();

                            var selectedQuestion = context.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);

                            if (selectedQuestion != null)
                            {
                                gbQuestion.Controls.Clear();

                                Label lblQuestion = new Label();
                                lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular);
                                lblQuestion.Text = $"Question {currentButtonNumber.ToString()}: {selectedQuestion.QuestionText}\n" +
                                                   $"A: {selectedQuestion.OptionA}\n" +
                                                   $"B: {selectedQuestion.OptionB}\n" +
                                                   $"C: {selectedQuestion.OptionC}\n" +
                                                   $"D: {selectedQuestion.OptionD}";
                                lblQuestion.Top = 20;
                                lblQuestion.Width = gbQuestion.Width - 40;
                                lblQuestion.Height = 200;
                                lblQuestion.AutoSize = false;

                                gbQuestion.Controls.Add(lblQuestion);

                                currentQuestionIndex = currentButtonNumber - 1;
                                LoadCheckboxStates(questions[currentQuestionIndex], currentQuestionIndex);
                                UpdateQuestionLabel();
                            }
                            else
                            {
                                MessageBox.Show("Question not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        };

                        Controls.Add(btnQuest);

                        // Adjust button positions
                        buttonLeft += buttonWidth + horizontalSpacing;
                        buttonNumber++;
                    }

                    // Update the lbQuestion label after creating buttons
                    UpdateQuestionLabel();
                }
                else
                {
                    MessageBox.Show("No questions found for the specified ExamId.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error loading questions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadQuestion(Question question, int currentButtonNumber)
        {
            // Load the specified question
            if (question != null)
            {
                // Clear existing controls in the group box
                gbQuestion.Controls.Clear();

                // Create a Label to display the concatenated string with larger font size and line breaks
                Label lblQuestion = new Label();
                lblQuestion.Font = new Font(lblQuestion.Font.FontFamily, 16, FontStyle.Regular); // Set font size to 16
                lblQuestion.Text = $"Question {currentButtonNumber + 1}: {question.QuestionText}\n" +
                                   $"A: {question.OptionA}\n" +
                                   $"B: {question.OptionB}\n" +
                                   $"C: {question.OptionC}\n" +
                                   $"D: {question.OptionD}";
                lblQuestion.Top = 20;
                lblQuestion.Width = gbQuestion.Width - 40; // Adjust width based on your design
                lblQuestion.Height = 200; // Adjust height based on your design
                lblQuestion.AutoSize = false; // Ensure the label size is fixed

                // Add the Label to the group box
                gbQuestion.Controls.Add(lblQuestion);

                // Load and set checkbox states
                LoadCheckboxStates(question, currentButtonNumber);
            }
            else
            {
                MessageBox.Show("Question not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Quiz_Load(object sender, EventArgs e)
        {
            var examCode = lbExamCode.Text;

            // Assuming you have a DbSet property named "Exams" in your DbContext
            var exam = context.Exams
                .Where(e => e.ExamName == examCode)
                .FirstOrDefault();
            LoadQuestionByExamCode(examCode);
            if (exam != null)
            {
                // Get the ExamId for the specified ExamCode
                int examId = exam.ExamId;

                // Create buttons for each question in the exam
                CreateQuestionButtonsForExam(examId);
            }
            else
            {
                MessageBox.Show("Exam not found for the specified ExamCode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Save the checkbox states for the current question
            SaveCheckboxStates(questions[currentQuestionIndex], currentQuestionIndex);

            // Check if any checkbox is checked for the current question
            bool isAnyCheckboxChecked = checkboxStates[currentQuestionIndex].Any(kv => kv.Value);

            // If any checkbox is checked, turn the corresponding button green
            if (isAnyCheckboxChecked)
            {
                Button currentButton = Controls.Find($"btnQuest{currentQuestionIndex + 1}", true).FirstOrDefault() as Button;
                if (currentButton != null)
                {
                    currentButton.BackColor = Color.Green;
                }
            }

            // Clear all checkboxes before loading the next question
            ClearAllCheckboxes();

            // Navigate to the next question
            if (questions != null && currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
            }
            else if (questions != null && questions.Any())
            {
                // If we reached the last question, go back to the first question
                currentQuestionIndex = 0;
            }
            else
            {
                MessageBox.Show("No questions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Return here to prevent further execution if there are no questions
            }

            // Load the question based on the updated index
            LoadQuestion(questions[currentQuestionIndex], currentQuestionIndex);

            // Load the checkbox states for the new question
            LoadCheckboxStates(questions[currentQuestionIndex], currentQuestionIndex);

            // Update the lbQuestion label
            UpdateQuestionLabel();
        }

        private void cbWarning_CheckedChanged(object sender, EventArgs e)
        {
            btnFinish.Enabled = true;
            if (cbWarning.Checked == false)
            {
                btnFinish.Enabled = false;
            }
        }
        private void UpdateQuestionLabel()
        {
            lbQuestion.Text = $"{currentQuestionIndex + 1}/{questions.Count}";
        }
        private void SaveCheckboxStates(Question question, int currentButtonNumber)
        {
            // Save checkbox states for the current question
            Dictionary<string, bool> questionCheckboxStates = new Dictionary<string, bool>();

            // Check for null before accessing properties
            if (cbA != null)
            {
                questionCheckboxStates["cbA"] = cbA.Checked;
            }

            if (cbB != null)
            {
                questionCheckboxStates["cbB"] = cbB.Checked;
            }

            if (cbC != null)
            {
                questionCheckboxStates["cbC"] = cbC.Checked;
            }

            if (cbD != null)
            {
                questionCheckboxStates["cbD"] = cbD.Checked;
            }

            // Save the dictionary to the main dictionary
            checkboxStates[currentButtonNumber] = questionCheckboxStates;
        }

        private void LoadCheckboxStates(Question question, int currentButtonNumber)
        {
            // Load checkbox states for the current question
            if (checkboxStates.TryGetValue(currentButtonNumber, out var questionCheckboxStates))
            {
                // Set checkbox states based on the saved states
                SetCheckboxState(cbA, "cbA", questionCheckboxStates);
                SetCheckboxState(cbB, "cbB", questionCheckboxStates);
                SetCheckboxState(cbC, "cbC", questionCheckboxStates);
                SetCheckboxState(cbD, "cbD", questionCheckboxStates);
            }
            else
            {
                // Initialize checkbox states if not found
                checkboxStates[currentButtonNumber] = new Dictionary<string, bool>();
            }
        }

        private void SetCheckboxState(CheckBox checkbox, string checkboxName, Dictionary<string, bool> questionCheckboxStates)
        {
            // Find the checkbox control by name and set its checked state
            if (checkbox != null)
            {
                if (questionCheckboxStates.TryGetValue(checkboxName, out var isChecked))
                {
                    checkbox.Checked = isChecked;
                }
            }
        }

        private void ClearAllCheckboxes()
        {
            // Check for null before accessing properties
            if (cbA != null)
            {
                cbA.Checked = false;
            }

            if (cbB != null)
            {
                cbB.Checked = false;
            }

            if (cbC != null)
            {
                cbC.Checked = false;
            }

            if (cbD != null)
            {
                cbD.Checked = false;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                // Save the checkbox states for the current question
                SaveCheckboxStates(questions[currentQuestionIndex], currentQuestionIndex);

                // Calculate the number of correct answers
                int correctAnswers = CalculateCorrectAnswers();

                // Calculate the mark
                double mark = (10.0 / questions.Count) * correctAnswers;

                // Display the mark
                MessageBox.Show($"Your mark: {mark}", "Quiz Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally, you can close the form or perform other actions here

                // Close the form
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                MessageBox.Show("Error calculating the mark: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CalculateCorrectAnswers()
        {
            int correctAnswers = 0;

            foreach (var question in questions)
            {
                // Retrieve the saved checkbox states for the question
                if (checkboxStates.TryGetValue(question.QuestionId - 1, out var questionCheckboxStates))
                {
                    // Check if the selected checkboxes match the correct answers in the database
                    bool isCorrect = IsCheckboxStateCorrect(questionCheckboxStates, question.CorrectAnswer);

                    if (isCorrect)
                    {
                        correctAnswers++;
                    }
                }
            }

            return correctAnswers;
        }

        private bool IsCheckboxStateCorrect(Dictionary<string, bool> checkboxStates, string correctAnswer)
        {
            // Determine if the selected checkboxes match the correct answer in the database
            switch (correctAnswer)
            {
                case "A":
                    return checkboxStates.TryGetValue("cbA", out var isCheckedA) && isCheckedA;
                case "B":
                    return checkboxStates.TryGetValue("cbB", out var isCheckedB) && isCheckedB;
                case "C":
                    return checkboxStates.TryGetValue("cbC", out var isCheckedC) && isCheckedC;
                case "D":
                    return checkboxStates.TryGetValue("cbD", out var isCheckedD) && isCheckedD;
                default:
                    return false;
            }
        }
    }
}
