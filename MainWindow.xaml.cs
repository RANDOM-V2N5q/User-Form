using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace UserForm {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private int editedItemIndex;
        private bool isModified;

        public MainWindow() {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Green;
            InitializeComponent();
            textBoxName.SetFocus();
            editedItemIndex = -1;
            isModified = false;
        }

        private bool IsNotEmpty( TextBoxWithErrorProvider textBox ) {
            if(textBox.Text.Trim() == "") {
                textBox.SetError("Pole nie może być puste");
                return false;
            }
            textBox.SetError("");
            return true;
        }

        private bool IsOnList(Person player ) {
            foreach(Person x in listBoxPlayers.Items) {
                if(x.IsTheSame(player)) {
                    return true;
                }
            }
            return false;
        }

        private void Window_Closing( object sender, System.ComponentModel.CancelEventArgs e ) {
            if(isModified) {
                MessageBoxResult result = MessageBox.Show($"Zapisać przed wyjściem?", "Info", MessageBoxButton.YesNoCancel);

                if(result == MessageBoxResult.Yes) {
                    Save(sender, new RoutedEventArgs());
                    e.Cancel = false;
                }
                else if(result == MessageBoxResult.No) {
                    e.Cancel = false;
                }
                else {
                    e.Cancel = true;
                }
            }
        }


        #region Buttons

        private void OK( object sender, RoutedEventArgs e ) {
            if(IsNotEmpty(textBoxName) & IsNotEmpty(textBoxSurname)) {
                if(editedItemIndex == -1) {
                    Person newPlayer = new Person(textBoxName.Text.Trim(), textBoxSurname.Text.Trim(), (uint)sliderWeight.Value, (uint)sliderAge.Value);
                    if(IsOnList(newPlayer)) {
                        MessageBox.Show($"Element już jest na liście", "Info", MessageBoxButton.OK);
                    }
                    else {
                        listBoxPlayers.Items.Add(newPlayer);
                        isModified = true;
                        Reset(sender, e);
                    }
                }
                else {
                    ((Person)listBoxPlayers.Items[editedItemIndex]).Name = textBoxName.Text.Trim();
                    ((Person)listBoxPlayers.Items[editedItemIndex]).Surname = textBoxSurname.Text.Trim();
                    ((Person)listBoxPlayers.Items[editedItemIndex]).Weight = (uint)sliderWeight.Value;
                    ((Person)listBoxPlayers.Items[editedItemIndex]).Age = (uint)sliderAge.Value;
                    listBoxPlayers.Items.Refresh();
                    editedItemIndex = -1;
                    isModified = true;
                    Reset(sender, e);
                }
            }
        }

        private void Reset( object sender, RoutedEventArgs e ) {
            textBoxName.Text = "";
            textBoxSurname.Text = "";
            sliderWeight.Value = 0;
            sliderAge.Value = 0;
            textBoxName.SetFocus();
            editedItemIndex = -1;
        }

        private void Delete( object sender, RoutedEventArgs e ) {
            if(listBoxPlayers.SelectedIndex != -1) {
                MessageBoxResult result = MessageBox.Show($"Czy chcesz ten element?\n{listBoxPlayers.SelectedItem}", "Info", MessageBoxButton.YesNo);

                if(result == MessageBoxResult.Yes) {
                    if(listBoxPlayers.SelectedIndex == editedItemIndex) {
                        editedItemIndex = -1;
                    }
                    listBoxPlayers.Items.RemoveAt(listBoxPlayers.SelectedIndex);
                    isModified = true;
                }
            }
        }

        private void Edit( object sender, RoutedEventArgs e ) {
            if(listBoxPlayers.SelectedIndex != -1) {
                Person player = listBoxPlayers.SelectedItem as Person;
                textBoxName.Text = player.Name;
                textBoxSurname.Text = player.Surname;
                sliderWeight.Value = player.Weight;
                sliderAge.Value = player.Age;
                editedItemIndex = listBoxPlayers.SelectedIndex;
                textBoxName.SetFocus();
            }
        }

        #endregion Buttons


        #region Menu

        private void Open( object sender, RoutedEventArgs e ) {
            if(isModified) {
                MessageBoxResult result = MessageBox.Show($"Zapisać bieżącą listę?", "Info", MessageBoxButton.YesNoCancel);

                if(result == MessageBoxResult.Yes) {
                    Save(sender, e);
                }
                else if(result == MessageBoxResult.No) {

                }
                else {
                    return;
                }
            }

            Person[] player;

            try {
                player = Archive.LoadFromFile();
            }
            catch {
                MessageBox.Show($"Niepoprawny format danych", "Info", MessageBoxButton.OK);
                return;
            }

            listBoxPlayers.Items.Clear();
            for(int i = 0; i < player.Length; i++) {
                listBoxPlayers.Items.Add(player[i]);
            }

            textBoxName.SetFocus();
            isModified = false;
        }

        private void Save( object sender, RoutedEventArgs e ) {
            ItemCollection collection = listBoxPlayers.Items;
            Person[] player = new Person[collection.Count];
            for(int i = 0; i < collection.Count; i++) {
                player[i] = collection[i] as Person;
            }
            Archive.SaveToFile(player);
            isModified = false;
            textBoxName.SetFocus();
        }

        private void Exit( object sender, RoutedEventArgs e ) {
            if(isModified) {
                MessageBoxResult result = MessageBox.Show($"Zapisać przed wyjściem?", "Info", MessageBoxButton.YesNoCancel);

                if(result == MessageBoxResult.Yes) {
                    Save(sender, e);
                    Environment.Exit(0);
                }
                else if(result == MessageBoxResult.No) {
                    Environment.Exit(0);
                }
            }
            else {
                Environment.Exit(0);
            }
        }

        #endregion Menu

    }
}