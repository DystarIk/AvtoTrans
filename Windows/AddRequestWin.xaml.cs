using System.ComponentModel.DataAnnotations;
using System.Windows;
using AvtoTrans.Enums;
using AvtoTrans.Models;

namespace AvtoTrans.Windows;


public partial class AddRequestWin : Window
{
    public Request Request { get; set; } = new();
    public string FIO { get; set; } = string.Empty;
    public IEnumerable<CarType> CarType { get; set; }


    public AddRequestWin()
    {
        InitializeComponent();
        DataContext = this;
        CarType = Enum.GetValues<CarType>().Cast<CarType>();
    }


    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Request.FIO = new FIO(FIO);
            Request.Date = DateTime.Now;
            Request.Status = RequestStatus.New;

            var context = new ValidationContext(Request);
            var result = new List<ValidationResult>();

            if (!Validator.TryValidateObject(Request, context, result, true))
            {
                string Error = "";
                foreach (var item in result)
                    Error += item + "\n";

                throw new Exception(Error);
            }

            using (Data.AppContext appContext = new())
            {
                appContext.Add(Request);
            }

            MessageBox.Show("Заявка добавлена");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}