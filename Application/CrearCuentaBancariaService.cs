using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearCuentaBancariaService
    {
        readonly IUnitOfWork _unitOfWork;
        
        public CrearCuentaBancariaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            CuentaBancaria cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {
                CuentaBancaria cuentaNueva = new CuentaAhorro();//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creó con exito la cuenta {cuentaNueva.Numero}." };
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }



    }
    public class CrearCuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public string Numero { get; set; }
    }
    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}
