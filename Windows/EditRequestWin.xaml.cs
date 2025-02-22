using System.ComponentModel.DataAnnotations;
using System.Windows;
using AvtoTrans.Enums;
using AvtoTrans.Models;

namespace AvtoTrans.Windows;

public partial class EditRequestWin : Window
{
    public Request Request { get; set; } = null!;
    public string FIO { get; set; } = string.Empty;

    public IEnumerable<CarType> CarTypes { get; set; }
    public IEnumerable<Mechanic> Mechanics { get; set; }
    public IEnumerable<RequestStatus> Statuses { get; set; }


    private Request _originallyRequest;


    public EditRequestWin(Request request)
    {
        InitializeComponent();


        using (Data.AppContext appContext = new())
        {
            Mechanics = appContext.Mechanics;
        }

        DataContext = this;

        _originallyRequest = request;

        Request = request.DeepCopy();
        FIO = Request!.FIO!.ToString()!;

        CarTypes = Enum.GetValues<CarType>().Cast<CarType>();
        Statuses = Enum.GetValues<RequestStatus>().Cast<RequestStatus>();
    }


    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Request.FIO = new FIO(FIO);
            var context = new ValidationContext(Request);
            var result = new List<ValidationResult>();

            string Error = "";
            if (!Validator.TryValidateObject(Request, context, result, true))
            {
                foreach (var item in result)
                    Error += item + "\n";

                throw new Exception(Error);
            }
            MessageBox.Show("Заявка изменилась");
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