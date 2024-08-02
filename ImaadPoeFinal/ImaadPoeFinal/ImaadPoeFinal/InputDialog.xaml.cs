using System;
using System.Windows;
using System.Windows.Controls;

namespace ImaadPoeFinal
{
    public partial class InputDialog : Window
    {
        // Property to store the prompt text
        public string Prompt { get; set; }

        // Property to store the dialog's title
        public string Title { get; set; }

        // Property to store the user input text
        public string InputText { get; set; }

        // Initializes a new instance of the InputDialog class.
        public InputDialog(string prompt, string title)
        {
            InitializeComponent(); // Initialize the components in the dialog
            Prompt = prompt; // Set the prompt text
            Title = title; // Set the dialog title
            DataContext = this; // Bind the data context to this instance

            // Center the dialog on the screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Handles the click event of the OK button.
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            InputText = InputTextBox.Text; // Capture the text from the input box
            DialogResult = true; // Set the dialog result to true, indicating successful input
        }

        // Handles the click event of the Cancel button.
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Set the dialog result to false, indicating cancellation
        }
    }
}
