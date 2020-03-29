using System;

namespace UserForm {
    internal class Person {
        #region Properties

        public string Name { get; set; }

        public string Surname { get; set; }

        public uint Age { get; set; }

        public uint Weight { get; set; }

        #endregion Properties


        #region Constructors

        public Person( string name, string surname, uint weight, uint age ) {
            Name = name;
            Surname = surname;
            Weight = weight;
            Age = age;
        }

        public Person(string data ) {
            string[] dataChunk = data.Split(';');
            if(dataChunk.Length != 4) {
                throw new Exception("Invalid data format");
            }

            Name = dataChunk[0];
            Surname = dataChunk[1];
            Weight = uint.Parse(dataChunk[2]);
            Age = uint.Parse(dataChunk[3]);
        }

        #endregion Constructors


        #region Methods

        public override string ToString() {
            return $"{Surname} {Name}\t\t waga: {Weight} kg\t lat: {Age}";
        }

        public string ToFileFormat() {
            return $"{Surname};{Name};{Weight};{Age}";
        }

        public bool IsTheSame( Person person ) {
            if(Name != person.Name) { return false; }
            if(Surname != person.Surname) { return false; }
            if(Weight != person.Weight) { return false; }
            if(Age != person.Age) { return false; }

            return true;
        }

        #endregion Methods
    }
}
