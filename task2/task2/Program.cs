using System;
using System.Reflection;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly namspc = Assembly.GetExecutingAssembly();
            Type[] clsName = namspc.GetTypes();
            foreach (var ins in clsName)
            {
                //Console.WriteLine("class name= {0}", ins.Name);
                if (ins.Name != "ReflectionUtility")
                {
                    var cons = ins.GetConstructor(new Type[0]);
                    var obj = cons.Invoke(new object[0]);
                    MethodInfo[] mtd = ins.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

                    foreach (var m_nam in mtd)
                    {
                        //Console.WriteLine("method name= {0}",m_nam.Name);
                        ParameterInfo[] parameters = m_nam.GetParameters();
                        int para_len = parameters.Length;
                        string[] arg = new string[para_len];
                        int trk = 0;
                        if (para_len != 0)
                            Console.WriteLine("please provide parameter value for method_name '{0}' like following sequence--->", m_nam.Name);
                        foreach (var para in parameters)
                        {
                            Console.Write("value for => {0}: ", para.Name);
                            arg[trk] = Console.ReadLine();
                            trk++;
                        }
                        //Console.WriteLine("perameter length= {0}", parameters.Length);
                        var utility_obj = new ReflectionUtility();
                        utility_obj.CallPrivate(obj, m_nam, arg);
                    }
                }
            }
        }
        private void programClassMethod(string name)
        {
            Console.WriteLine("\nprivate method from program class is called for printing name: {0}\n", name);
        }
    }

    public class ReflectionUtility
    {
        public void CallPrivate(object targetObject, MethodInfo methodName, object[] args)
        {
            //MethodInfo fun=targetObject.GetMe
            methodName.Invoke(targetObject, args);
        }
    }

    public class ClassWithPrivateAndProtectedMethod
    {
        private void Print(string name)
        {
            Console.WriteLine("\nprivate method is called for printing name: {0}\n",name);
        }

        protected void Display(string name)
        {
            Console.WriteLine("\nprotected method is called for printing name:{0}\n", name);
        }
    }

}
