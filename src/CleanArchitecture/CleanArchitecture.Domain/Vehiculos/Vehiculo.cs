using System.Net.Mail;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
  public Vehiculo(
    Guid id,
    Model model,
    Vin vin,
    Currency price,
    Currency manteinance,
    DateTime dateLastRent,
    List<Accessory> accesories,
    Address address
  ) : base(id)
  {
    Model = model;
    Vin = vin;
    Price = price;
    Manteinance = manteinance;
    DateLastRent = dateLastRent;
    Accesories = accesories;
    Address = address;
  }

  public Model? Model { get; private set; }
  public Vin? Vin { get; private set; }
  public Address? Address { get; private set; }

  public Currency? Price { get; private set; }
  public Currency? Manteinance { get; private set; }

  public DateTime? DateLastRent { get; internal set; }

  public List<Accessory> Accesories { get; private set; } = new();

}