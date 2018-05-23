using HotelEvents;
using HotelSimulatie.Areas;
using HotelSimulatie.People;
using HotelSimulatie.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulatie
{
    public partial class MenuForm : Form
    {
        /// <summary>
        /// All the customers to be displayed
        /// </summary>
        public List<Customer> Customers { get; set; }
        /// <summary>
        /// The cleaners to be displayed
        /// </summary>
        public List<Cleaner> Cleaners { get; set; }
        /// <summary>
        /// a list with all persons
        /// </summary>
        public List<IPerson> Persons { get; set; }
        /// <summary>
        /// The hotel of wich the areas' status should be displayed
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// The stairs which's speed should be changable
        /// </summary>
        public Stairs Stairs { get; set; }
        /// <summary>
        /// The graph, needs new edges in stairs if stairs is updated
        /// </summary>
        public SimplePath SimplePath { get; set; }
        /// <summary>
        /// The time a movie takes to complete
        /// </summary>
        public int MovieTime { get; set; }
        /// <summary>
        /// How fast people eat
        /// </summary>
        public int EatingSpeed { get; set; }
        /// <summary>
        /// How fast cleaners clean
        /// </summary>
        public int CleanSpeed { get; set; }
        /// <summary>
        /// Initialize the properties of the menuform
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="cleaners"></param>
        /// <param name="customers"></param>
        /// <param name="persons"></param>
        /// <param name="stairs"></param>
        /// <param name="simplePath"></param>
        public MenuForm(Hotel hotel, List<Cleaner> cleaners, List<Customer> customers, List<IPerson> persons, Stairs stairs, SimplePath simplePath)
        {
            Customers = customers;
            Cleaners = cleaners;
            Persons = persons;
            Hotel = hotel;
            Stairs = stairs;
            SimplePath = simplePath;
            InitializeComponent();
            InitMenu();
            MovieTime = 50;
            EatingSpeed = 10;
            CleanSpeed = 10;
        }
        private void InitMenu()
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            InitCmbHTE_Time();
            InitCmbStairsSpeed();
            void InitCmbHTE_Time()
            {
                cmbHTE_Time.Items.Clear();
                cmbHTE_Time.Items.Add("1");
                cmbHTE_Time.Items.Add("2");
                cmbHTE_Time.Items.Add("3");
            }
            InitCmbHTE_Time();
            void InitCmbStairsSpeed()
            {
                cmbStairsSpeed.Items.Clear();
                cmbStairsSpeed.Items.Add("1");
                cmbStairsSpeed.Items.Add("2");
                cmbStairsSpeed.Items.Add("3");
                cmbStairsSpeed.Items.Add("4");
                cmbStairsSpeed.Items.Add("5");
            }
        }
        /// <summary>
        /// Refreshes the tabs with the updated information
        /// </summary>
        public void ReInitListboxes()
        {
            UpdateRoomStatus();
            UpdateCustomerStatus();
            UpdateCleanerStatus();
            void UpdateRoomStatus()
            {
                listBoxRoomStatus.Items.Clear();
                foreach (Area area in Hotel.Areas.OrderBy(t => t.AreaType))
                {
                    listBoxRoomStatus.Items.Add(area);
                }
            }
            void UpdateCustomerStatus()
            {
                listBoxCustomerStatus.Items.Clear();
                foreach (Customer customer in Customers)
                {
                    listBoxCustomerStatus.Items.Add(customer);
                }
                lblCustomerCount.Text = "Customer Count: " + listBoxCustomerStatus.Items.Count;
            }
            void UpdateCleanerStatus()
            {
                listBoxCleanerStatus.Items.Clear();
                foreach (Cleaner cleaner in Cleaners)
                {
                    listBoxCleanerStatus.Items.Add(cleaner);
                }
            }
        }
        private void CloseOnClick(object sender, EventArgs e)
        {
            this.Hide();
            if (cmbHTE_Time.Text != "")
            {                
                HotelEventManager.HTE_Factor = (float)Math.Pow(2, Convert.ToInt32(cmbHTE_Time.Text) - 1);
            }
            if (cmbStairsSpeed.Text != "")
            {
                Stairs.Weight = -(Convert.ToInt32(cmbStairsSpeed.Text) - 6);
            }
            if (cmbStairsSpeed.Text != "")
            {
                SimplePath = new SimplePath();
                Hotel.AddToGraph(SimplePath);
            }
            if(Movie_Runtime_TXT.Text != "")
            {
                int parsedValue;
                if (int.TryParse(Movie_Runtime_TXT.Text, out parsedValue))
                {

                    MovieTime = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(Movie_Runtime_TXT.Text, "[^\\d]")));
                    foreach (Cinema cinema in Hotel.Areas.Where(r => r.AreaType == "Cinema"))
                    {
                        Cinema TempCin = (Cinema)cinema;
                        TempCin.RunTime = MovieTime;
                    }
                }
            }
            if(Eating_Speed_TXT.Text != "")
            {
                int parsedValue;
                if (int.TryParse(Eating_Speed_TXT.Text, out parsedValue))
                {
                    EatingSpeed = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(Eating_Speed_TXT.Text, "[^\\d]")));
                    foreach (Restaurant restaurant in Hotel.Areas.Where(r => r.AreaType == "Restaurant"))
                    {
                        Restaurant TempRes = (Restaurant)restaurant;
                        TempRes.EatSpeed = EatingSpeed;
                    }
                }
            }
            if(Cleaning_Speed_TXT.Text != "")
            {
                int parsedValue;
                if(int.TryParse(Cleaning_Speed_TXT.Text, out parsedValue))
                {
                    CleanSpeed = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(Cleaning_Speed_TXT.Text, "[^\\d]")));
                    foreach(Cleaner cleaner in Cleaners)
                    {
                        cleaner.CleaningSpeed = CleanSpeed;
                    }
                }
            }
            foreach (IPerson person in Persons)
            {
                person.RoundPosition();
            }
            if (!HotelEventManager.Running)
            {
                HotelEventManager.Pauze();
            }            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
