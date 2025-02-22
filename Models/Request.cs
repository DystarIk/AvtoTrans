using System.ComponentModel.DataAnnotations;
using AvtoTrans.Enums;

namespace AvtoTrans.Models;

public class Request
{
    /// <summary>
    /// Номер заявки
    /// </summary>
    public uint? Number { get; set; }

    /// <summary>
    /// Дата
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Тип автомобиля
    /// </summary>
    [Required(ErrorMessage = "Поле «Тип авто» пустое")]
    public CarType? CarType { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    [Required(ErrorMessage = "Поле «Модель авто» пустое")]
    public string? CarModel { get; set; }

    /// <summary>
    /// Описание проблемы
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    [Required(ErrorMessage = "Поле «ФИО» пустое")]
    public FIO? FIO { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required(ErrorMessage = "Поле «Телефон» пустое")]
    [RegularExpression(@"^\+?[1-9]\d{1,14}", ErrorMessage = "Некорректный формат телефона")]
    public string? Phone { get; set; }

    /// <summary>
    /// Статус заявки
    /// </summary>
    public RequestStatus? Status { get; set; }

    /// <summary>
    /// Механик
    /// </summary>
    public Mechanic? Mechanic { get; set; }


    /// <summary>
    /// Глубокое копирование 
    /// </summary>
    public Request DeepCopy()
    {
        return new()
        {
            Date = Date,
            Number = Number,
            Status = Status,

            Phone = Phone,
            FIO = new FIO(FIO!.ToString()!),

            Mechanic = Mechanic != null 
                ? new Mechanic(Mechanic.FIO) 
                : null,

            CarType = CarType,
            CarModel = CarModel,
            Description = Description,
        };
    }


    public override string ToString()
    {
        string mechanic = Mechanic?.FIO.ToString() ?? "Не назначен";

        return $"Номер: {Number} Клиент: {FIO} Статус: {Status} Механик{mechanic}";
    }
}