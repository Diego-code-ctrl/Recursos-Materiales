using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recursos_Materiales.Interfacez;

namespace Recursos_Materiales.Clases
{
    public static class Llamadas
    {
        public static void LlamarInicio()
        {
            Inicio ini = new Inicio();
            ini.Show();
        }

        public static void LlamarAdmin()
        {
            Admin admin = new Admin();
            admin.Show();
        }

        public static void LlamarAgDep()
        {
            AgDep agDep = new AgDep();
            agDep.Show();
        }

        public static void LlamarAgMat()
        {
            AgMat agMat = new AgMat();
            agMat.Show();
        }

        public static void LlamarCheckPedidos()
        {
            ChekPedido chekPedido = new ChekPedido();
            chekPedido.Show();
        }

        public static void LlamarConDep()
        {
            ConDep conDep = new ConDep();
            conDep.Show();
        }

        public static void LlamarConMat()
        {
            ConMat conMat = new ConMat();
            conMat.Show();
        }

        public static void LlamarEldep()
        {
            EliDep elDep = new EliDep();
            elDep.Show();
        }

        public static void LlamarEliMat()
        {
            EliMat eliMat = new EliMat();
            eliMat.Show();
        }

        public static void LlamarLogin()
        {
            Login login = new Login();
            login.Show();
        }

        public static void LlamarPedido()
        {
            Pedido pedido = new Pedido();
            pedido.Show();
        }

        public static void LlamarVerPedidos()
        {
            VerPedidos verPedidos = new VerPedidos();
            verPedidos.Show();
        }
    }
}
