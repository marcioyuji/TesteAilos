using System.Globalization;

namespace Questao1
{
    public class ContaBancaria {

        public int Numero { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; set; }

        public ContaBancaria(int numero, string titular, double saldo = 0)
        {
            Numero = numero;
            Titular = titular;
            Saldo = saldo;
        }
        public void Deposito(double deposito)
        {
            Saldo += deposito;
        }

        public void Saque(double saque)
        {
            Saldo -= saque + 3.50;
        }
    }
}
