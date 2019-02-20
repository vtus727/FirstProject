using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlockIncomingCall
{
    public partial class MainPage : ContentPage
    {

        PersonView pw;
        public List<Person> Persons;

        public PersonDatabase memberDatabase;
        public Person member;
        public MainPage()
        {
            InitializeComponent();
            pw = new PersonView();
            Persons = pw.GetListPerson();

            memberDatabase = new PersonDatabase();
            var members = memberDatabase.GetMembers();
            listPerson.ItemsSource = members;

            btnXoa.IsEnabled = false;



            // xử lý thao tác click listView
            listPerson.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = (Person)e.SelectedItem;
                txtSDT.Text = item.Phone;
                txtTen.Text = item.PersonName;
                member = item;
                btnXoa.IsEnabled = true;
            };
        }





        // load dữ liệu lên listview
        void loadData()
        {
            try
            {
                memberDatabase = new PersonDatabase();
                var members = memberDatabase.GetMembers();
                listPerson.ItemsSource = members;
            }
            catch (Exception e)
            {
                DisplayAlert("Error", e.StackTrace.ToString(), "OK");
            }
        }


        // btnThem event
        void OnButtonClicked(object sender, EventArgs args)
        {

            member = new Person();
            memberDatabase = new PersonDatabase();
            member.PersonName = txtTen.Text;
            member.Phone = txtSDT.Text;
            memberDatabase.AddMember(member);

            txtSDT.Text = String.Empty;
            txtTen.Text = String.Empty;
            var members = memberDatabase.GetMembers();
            listPerson.ItemsSource = members;
            btnXoa.IsEnabled = false;
        }


        //btnXoaClick
        async void btnXoaClickAsync(object sender, EventArgs args)
        {
            bool tl = await DisplayAlert("Thông báo", "Bạn muốn xóa liên hệ này", "Có", "Không");

            if (tl)
            {
                txtSDT.Text = String.Empty;
                txtTen.Text = String.Empty;
                memberDatabase.DeleteMember(member.ID);
                loadData();
                btnXoa.IsEnabled = false;
            }

        }
    }
}
