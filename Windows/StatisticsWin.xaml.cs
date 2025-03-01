using System.Collections.ObjectModel;
using System.Windows;
using AvtoTrans.Enums;
using AvtoTrans.Models;

namespace AvtoTrans.Windows;

public partial class StatisticsWin : Window
{
    public int New { get; set; }
    public int InProcess { get; set; }
    public int Completed { get; set; }
    public ObservableCollection<Mechanic> Mechanics { get; set; } = new();
    public Mechanic SelectMechanic { get; set; }


    public StatisticsWin(ObservableCollection<Request>? requests)
    {
        InitializeComponent();
        DataContext = this;

        New = requests.Where(r => r.Status == RequestStatus.New).Count();
        InProcess = requests.Where(r => r.Status == RequestStatus.InProcess).Count();
        Completed = requests.Where(r => r.Status == RequestStatus.Completed).Count();

        using (Data.AppContext ctx = new())
        {
            foreach (Request request in requests)
            {
                if (request.Mechanic != null)
                {
                    ctx.AddMechanic(request.Mechanic, request);
                }
            }

            foreach (Mechanic mechanic in ctx.Mechanics)
                Mechanics.Add(mechanic);
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string str = "";
        foreach(var item in SelectMechanic.Requests)
        {
            str += item.ToString();
        }

        MessageBox.Show(str);

    }
}