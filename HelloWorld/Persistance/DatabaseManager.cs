using HelloWorld.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace HelloWorld.Persistance
{
    public sealed class DatabaseManager
    {
        private static DatabaseManager instance = null;
        private static readonly object locker = new object();
        private readonly SQLiteConnection _connection;

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseManager();
                        }
                    }
                }
                return instance;
            }
        }

        private DatabaseManager()
        {
            _connection = DependencyService.Get<ISqliteDb>().GetConnection();
            CreateDatabase();
            var acronym = _connection.GetAllWithChildren<Acronym>().FirstOrDefault();
            if (acronym == null)
            {
                SeedDatabase();
            }
        }

        private void SeedDatabase()
        {
            var acronyms = new List<Acronym>
            {
                new Acronym{ Name = "AH", TranslationEnglish = "Authentication Header", TranslationPolish = "nagłowek uwierzytelniania", Type = Category.Sieci},
                new Acronym{ Name = "API", TranslationEnglish = "Application Programming Interface", TranslationPolish = "interfejs programów użytkowych", Type = Category.Sieci},
                new Acronym{ Name = "ARP", TranslationEnglish = "Address Resolution Protocol", TranslationPolish = "protokół odwzorowywania adresów", Type = Category.Sieci},
                new Acronym{ Name = "ASCII", TranslationEnglish = "American Standard Code for Information Interchange", TranslationPolish = "standardowy amerykański kod wymiany informacji ", Type = Category.Sieci},
                new Acronym{ Name = "ASIC", TranslationEnglish = "Application Specific Integrated Circuit", TranslationPolish = "układ scalony właściwy aplikacji", Type = Category.Sieci},
                new Acronym{ Name = "AS", TranslationEnglish = "Autonomous System", TranslationPolish = "system autonomiczny", Type = Category.Sieci},
                new Acronym{ Name = "ATM", TranslationEnglish = "Asynchronous Transfer Mode", TranslationPolish = "tryb przesyłania asynchronicznego", Type = Category.Sieci},
                new Acronym{ Name = "B8ZS", TranslationEnglish = "Bipolar with 8-Zeros Substitution", TranslationPolish = "bipolarna substytucja ośmiozerowa", Type = Category.Sieci},
                new Acronym{ Name = "BECN", TranslationEnglish = "Backward Explicit Congestion", TranslationPolish = "jawne powiadomienie o zatorze wysyłane w kierunku nadawcy", Type = Category.Sieci},
                new Acronym{ Name = "bps", TranslationEnglish = "bits per second", TranslationPolish = "bity na sekundę", Type = Category.Sieci},
                new Acronym{ Name = "BRA", TranslationEnglish = "Basic Rate Access", TranslationPolish = "dostęp w trybie podstawowym", Type = Category.Sieci},
                new Acronym{ Name = "BPDU", TranslationEnglish = "Bridge Protocol Data Unit", TranslationPolish = "jednostka danych protokołu mostu", Type = Category.Sieci},
                new Acronym{ Name = "CIR", TranslationEnglish = "Committed Information Rate", TranslationPolish = "przepustowość (średnia) gwarantowana umową ", Type = Category.Sieci},
                new Acronym{ Name = "CBR", TranslationEnglish = "Committed Burst Rate", TranslationPolish = "przepustowość chwilowa gwarantowana umową", Type = Category.Sieci},
                new Acronym{ Name = "CHAP", TranslationEnglish = "Challenge Handshake Authentication Protocol", TranslationPolish = "protokół wymiany wyzwania uwierzytelniającego (protokół uwierzytelniania przez uzgodnienie)", Type = Category.Sieci},
                new Acronym{ Name = "CIDR", TranslationEnglish = "Classless InterDomain Routing", TranslationPolish = "bezklasowy routing międzydomenowy", Type = Category.Sieci},
                new Acronym{ Name = "CIFS", TranslationEnglish = "Common Internet File System", TranslationPolish = "powszechny internetowy system plików", Type = Category.Sieci},
                new Acronym{ Name = "CIR", TranslationEnglish = "Committed Information Rate", TranslationPolish = "zagwarantowany poziom transmisji", Type = Category.Sieci},
                new Acronym{ Name = "CoS", TranslationEnglish = "Class Of Service", TranslationPolish = "klasa usługi", Type = Category.Sieci},
                new Acronym{ Name = "CoS", TranslationEnglish = "Customer-premises Equipment", TranslationPolish = "zakończenie sieci telekomunikacyjnej znajdujące się u klienta, terminal", Type = Category.Sieci},
                new Acronym{ Name = "CSMA/CD", TranslationEnglish = "Carrier Sense-Multiple Access/Collision Detection", TranslationPolish = "wielodostęp z wykrywaniem fali nośnej i wykrywaniem kolizji", Type = Category.Sieci},
                new Acronym{ Name = "CRC", TranslationEnglish = "Cyclic Redundancy Check", TranslationPolish = "cykliczna kontrola nadmiarowa", Type = Category.Sieci},

                new Acronym{ Name = "DRAM", TranslationEnglish = "Dynamic RAM", TranslationPolish = "dynamiczna pamięć RAM", Type = Category.Systemy},
                new Acronym{ Name = "EDVAC", TranslationEnglish = "Electronic Discrete", TranslationPolish = "Variable Computer", Type = Category.Systemy},
                new Acronym{ Name = "EEPROM", TranslationEnglish = "Electrically Erasable and Programmable", TranslationPolish = "wymazywalnai programowalna pamięć stała", Type = Category.Systemy},
                new Acronym{ Name = "EIDE", TranslationEnglish = "Extended Integrated Device Electronics", TranslationPolish = "scalona elektronika obsługidysku", Type = Category.Systemy},
                new Acronym{ Name = "EISA", TranslationEnglish = "Extended Industry Standard Architecture", TranslationPolish = "rozszerzona standardowaarchitektura przemysłowa", Type = Category.Systemy},
                new Acronym{ Name = "EMS", TranslationEnglish = "Expanded Memory System", TranslationPolish = "układ pamięci rozszerzonej", Type = Category.Systemy},
                new Acronym{ Name = "ENIAC", TranslationEnglish = "Electronic Numerical Integrator and Computer", TranslationPolish = "elektroniczne urządzenie numeryczne całkujące i liczące", Type = Category.Systemy},
                new Acronym{ Name = "EPROM", TranslationEnglish = "Erasable and Programmable ROM", TranslationPolish = "wymazywalna i programowalnapamięć stała", Type = Category.Systemy},
                new Acronym{ Name = "EXT2", TranslationEnglish = "Second Extended File System", TranslationPolish = "drugi rozszerzony system plików", Type = Category.Systemy},
                new Acronym{ Name = "EXT3", TranslationEnglish = "Third Extended File System", TranslationPolish = "trzeci rozszerzony system plików", Type = Category.Systemy},
                new Acronym{ Name = "EXT4", TranslationEnglish = "Fourth Extended File System", TranslationPolish = "czwarty rozszerzony system plików", Type = Category.Systemy},
                new Acronym{ Name = "FAT", TranslationEnglish = "File Allocation", TranslationPolish = "tablica alokacji plików", Type = Category.Systemy}         
            };

            _connection.InsertAll(acronyms);
        }

        public List<T> GetALL<T>() where T : class, new()
        {
            return _connection.GetAllWithChildren<T>().ToList();
        }

        private void CreateDatabase()
        {
            _connection.CreateTable<Acronym>();
        }

        public void Add<T>(T objectToAdd)
        {
            _connection.Insert(objectToAdd);
        }

        public void RemoveFromDatabase<T>(int id)
        {
            _connection.Delete<T>(id);
        }
    }
}
