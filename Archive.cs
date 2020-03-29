using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace UserForm {
    class Archive {
        public static void SaveToFile( Person[] player ) {
            using(SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                if(saveFileDialog.ShowDialog() == DialogResult.OK) {
                    Stream outputStream = saveFileDialog.OpenFile();

                    using(StreamWriter writer = new StreamWriter(outputStream)) {
                        if(player.Length > 0) {
                            writer.Write($"{player[0].ToFileFormat()}");
                        }
                        for(int i = 1; i < player.Length; i++) {
                            writer.Write($"\n{player[i].ToFileFormat()}");
                        }
                    }
                }
            }
        }

        public static Person[] LoadFromFile() {
            string[] fileContent = new string[0];

            using(OpenFileDialog openFileDialog = new OpenFileDialog()) {
                if(openFileDialog.ShowDialog() == DialogResult.OK) {
                    Stream inputStream = openFileDialog.OpenFile();

                    using(StreamReader reader = new StreamReader(inputStream)) {
                        fileContent = reader.ReadToEnd().Split('\n');
                    }
                }
            }

            List<Person> player = new List<Person>();
            for(int i = 0; i < fileContent.Length; i++) {
                if(fileContent[i].Length != 0) {
                    player.Add(new Person(fileContent[i]));
                }
            }
            return player.ToArray();
        }
    }
}