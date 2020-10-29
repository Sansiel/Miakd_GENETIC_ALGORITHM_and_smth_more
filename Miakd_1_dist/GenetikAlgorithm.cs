using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miakd_1_dist
{
    public class GenetikAlgorithm
    {
        int[][] Oi;//массив объектов и их характеристик
        int[] T;// массив требований
        int ko; //количество объектов
        int kt;// количество требований
        int maxkr;//максимальное отклонение от требований
        int minkr;//минимальное отклонение от требований
        int kolos;//количество особей в популяции
        byte[][] Popul;//популяция
        double[] Prisp;//приспособленность особей
        double[] cen;//фитнес-функция для каждого объекта
        int MaxIter;//максимум итераций
        int[] Os_for_kross;// хромосомы для кроссовера

        public GenetikAlgorithm()
        {
            T = new int[] { 50, 60, 70, 80, 90, 100 };
            Oi = new int[][] { { 20, 30, 40, 50, 60, 70 }, { 40, 40, 40, 40, 40, 40 }, { 30, 30, 30, 30, 30, 30 }, { 20, 30, 40, 40, 30, 30 } };
            ko = 4;
            kt = 6;
            maxkr = 80;
            minkr = 40;
            kolos = 8;
            MaxIter = 10000;
            Popul = new byte[(int)Math.pow(2, kolos) / 2][ko];
            Prisp = new double[(int)Math.pow(2, kolos) / 2];
            cen = new double[ko];
            Os_for_kross = new int[(int)Math.pow(2, kolos) / 2];
        }
        //Теперь определим метод инициализации начальной популяции:
        public void initPop()
        {
            for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
                for (int j = 0; j < ko; j++)
                    if (i + j == kolos + 2)
                        Popul[i][j] = 1;
                    else Popul[i][j] = 0;
        }
        //Метод вычисления фитнесс-функции:
        public void Cen()
        {
            for (int i = 0; i < ko; i++)
            {
                int sum = 0;
                for (int j = 0; j < kt; j++)
                    sum = sum + (T[j] - Oi[i][j]);
                cen[i] = sum / 10;
            }
        }
        //Метод оценки приспособленности хромосом в популяции:
        public void OcPrisp()
        {
            for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
            {
                double sum = 0;
                for (int j = 0; j < ko; j++)
                {
                    sum = sum + Popul[i][j] * cen[j];
                }
                Prisp[i] = sum;
            }
        }
        //Ранговый метод выбора особей для кроссовера:
        public void Select()
        {
            double[] Vert = new double[(int)Math.pow(2, kolos) / 2];
            double sum = 0;
            for (int j = 0; j < ko; j++)
                sum = sum + cen[j];
            for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
                Vert[i] = Prisp[i] / sum;
            for (int t = 0; t < Math.pow(2, kolos) / 2; t++)
            {
                double max = -1;
                int m = 0;
                for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
                    if (Vert[i] > max)
                    {
                        max = Vert[i];
                        m = i;
                    }
                Vert[m] = -1;
                Os_for_kross[t] = m;
            }
        }
        //Метод, выполняющий мутацию:
        public void mutation()
        {
            for (int j = 0; j < Math.pow(2, kolos) / 2; j++)
            {
                int l = 0;
                int k = 0;
                while (l == 0)
                {
                    k = (int)Math.round(Math.random() * 10 % 3);
                    if (k == 0) l = 0;
                    else
                if (k > ko) l = 0; else l = 1;
                }
                if (Math.random() > 0.5)
                    Popul[j][k] = (byte)(1 - Popul[j][k]);
            }
        }
        //Метод, выполняющий кроссовер:
        public void krossover()
        {
            int lg = 0;
            int k = 0;
            while (lg == 0)
            {
                k = (int)(Math.round(Math.random() * 10 % 3));
                if (k == 0) { lg = 0; }
                else
                {
                    if (k >= ko) { lg = 0; }
                    else { lg = 1; }
                }
            }
            int l = Os_for_kross.length;
            int t = 0;
            while (t < l - 1)
            {
                for (int i = 0; i < k; i++)
                {
                    byte tmp = Popul[Os_for_kross[t]][i];
                    Popul[Os_for_kross[t]][i] = Popul[Os_for_kross[t + 1]][i];
                    Popul[Os_for_kross[t + 1]][i] = tmp;
                }
                for (int i = k + 1; i < ko; i++)
                {
                    byte tmp = Popul[Os_for_kross[t]][i]; Popul[Os_for_kross[t]][i] =
                      Popul[Os_for_kross[t + 1]][i];
                    Popul[Os_for_kross[t + 1]][i] = tmp;
                }
                t += 2;
            }
        }
        //Метод проверки удовлетворения хромосомы условию:
        public boolean Prov()
        {
            double max = maxkr;
            for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
                if ((Prisp[i] <= max) & (Prisp[i] > 0))
                { max = Prisp[i]; }
            return !((max >= minkr) & (max <= maxkr));
        }
        //Метод вывода результата:
        public String vivod()
        {
            String s = "";
            for (int i = 0; i < Popul.length; i++)
            {
                for (int jj = 0; jj < Popul[i].length; jj++)
                    s += (Popul[i][jj] + " ");
                s += "\n";
            }
            s += "----" + Prisp.length + "----" + "\n";
            for (int k = 0; k < Prisp.length; k++)
                s += (Prisp[k] + " ");
            s += "----" + "\n";
            int j = 0;
            double max = maxkr;
            for (int i = 0; i < Math.pow(2, kolos) / 2; i++)
            {
                if ((Prisp[i] <= max) && (Prisp[i] >= minkr))
                {
                    max = Prisp[i];
                    j = i;
                    s += "chmax---" + max + "---" + j + "----" + "\n";
                }
            }
            s += "---" + j + "----" + "\n";
            s += "Rezult!!!!";
            for (int i = 0; i < ko; i++)
            {
                s += Popul[j][i] + " ";
            }
            s += "Rezult!!!!";
            s += "----" + "\n";
            s += (max) + "\n";
            return s;
        }
        //Основной метод работы генетического алгоритма:
        public String stdga()
        {
            initPop();
            Cen();
            boolean lg = true;
            int step = 0;
            while (lg)
            {
                OcPrisp();
                Select();
                krossover();
                mutation();
                OcPrisp();
                lg = Prov();
                step++;
                if (step == MaxIter) lg = false;
            }
            return vivod();
        }
    }
}
