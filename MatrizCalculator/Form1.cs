﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrizCalculator
{
    public partial class Form1 : Form
    {
        // As variaveis que armazenam cada uma das 4 matrizes
        TextBox[,] MO = new TextBox[1, 1];
        TextBox[,] MF = new TextBox[1, 1];
        TextBox[,] MA = new TextBox[1, 1];
        TextBox[,] MB = new TextBox[1, 1];
        TextBox[,] MC = new TextBox[1, 1];
        Label[] Coordenadas = new Label[1];

        // pegando propriedades para desenho
        System.Drawing.Graphics formGraphics;
        System.Drawing.Pen myPen;
        public Form1()
        {
            InitializeComponent();
            // gerar as matrizes vazias 2x2 iniciais
            ReloadA(null);
            ReloadB(null);
            ReloadO(null);
            // criar a pagina de desenho
            formGraphics = this.tabPage5.CreateGraphics();
           
        }
        // função para gerar os TextBox da matriz A
        /*explicacao
        pode receber os valores anteriores
        pega o tamanho das colunas e das linhas que estão definados no "NUD" e coloca como o tamanho da matriz
        para cada valor da matriz é criado um text box que é adicionado a pagina certa
        caso tenha valores anteriores, ele os coloca na matriz*/
        void ReloadA(TextBox[,] lastValues)
        {
            MA = new TextBox[int.Parse(M1C.Value.ToString()), int.Parse(M1L.Value.ToString())];
            for (int i = 0; i < M1C.Value; i++)
            {
                for (int j = 0; j < M1L.Value; j++)
                {
                    MA[i, j] = new TextBox();
                    MA[i, j].Location = new Point(M1L.Location.X + i * 40, 100 + j * 30);
                    MA[i, j].Width = 30;
                    tabPage1.Controls.Add(MA[i, j]);
                    try
                    {
                        MA[i, j].Text = lastValues[i, j].Text;
                    }
                    catch { }
                }
            }
        }
        // função para gerar os TextBox da matriz O
        void ReloadO(TextBox[,] lastValues)
        {
            MO = new TextBox[int.Parse(M1C_.Value.ToString()), int.Parse(M1L_.Value.ToString())];
            for (int i = 0; i < M1C_.Value; i++)
            {
                for (int j = 0; j < M1L_.Value; j++)
                {
                    MO[i, j] = new TextBox();
                    MO[i, j].Location = new Point(M1L_.Location.X + i * 40, 100 + j * 30);
                    MO[i, j].Width = 30;
                    tabPage3.Controls.Add(MO[i, j]);
                    try
                    {
                        MO[i, j].Text = lastValues[i, j].Text;
                    }
                    catch { }
                }
            }
        }
        // função para gerar os TextBox da matriz B
        void ReloadB(TextBox[,] lastValues)
        {
            MB = new TextBox[int.Parse(M2C.Value.ToString()), int.Parse(M2L.Value.ToString())];
            for (int i = 0; i < M2C.Value; i++)
            {
                for (int j = 0; j < M2L.Value; j++)
                {
                    MB[i, j] = new TextBox();
                    MB[i, j].Location = new Point(M2L.Location.X + i * 40, 100 + j * 30);
                    MB[i, j].Width = 30;
                    tabPage1.Controls.Add(MB[i, j]);
                    try
                    {
                        MB[i, j].Text = lastValues[i, j].Text;
                    }
                    catch { }
                }
            }
        }
        // função para gerar os TextBox da matriz C
        /*diferenças entre as outras
        não recebe valores anteriores
        remove as textbox dos resultados anteriores*/
        void ReloadC()
        {
            foreach (TextBox text in MC)
            {
                tabPage1.Controls.Remove(text);
            }
            MC = new TextBox[int.Parse(M3C.Text), int.Parse(M3L.Text)];
            for (int i = 0; i < int.Parse(M3C.Text); i++)
            {
                for (int j = 0; j < int.Parse(M3L.Text); j++)
                {
                    MC[i, j] = new TextBox();
                    MC[i, j].Location = new Point(M3L.Location.X + i * 40, 100 + j * 30);
                    MC[i, j].Width = 30;
                    tabPage1.Controls.Add(MC[i, j]);
                }
            }
        }
        // função para gerar os TextBox da matriz F
        void ReloadF()
        {
            foreach (TextBox text in MF)
            {
                tabPage3.Controls.Remove(text);
            }
            MF = new TextBox[int.Parse(M3C_.Text), int.Parse(M3L_.Text)];
            for (int i = 0; i < int.Parse(M3C_.Text); i++)
            {
                for (int j = 0; j < int.Parse(M3L_.Text); j++)
                {
                    MF[i, j] = new TextBox();
                    MF[i, j].Location = new Point(M3L_.Location.X + i * 40, 100 + j * 30);
                    MF[i, j].Width = 30;
                    tabPage3.Controls.Add(MF[i, j]);
                }
            }
        }
        // atualizar o tamanho da Matriz A no Outros
        private void ChangeLenghtOther(object sender, EventArgs e)
        {
            TextBox[,] MT = MO;
            foreach (TextBox text in MO)
            {
                tabPage3.Controls.Remove(text);
            }
            ReloadO(MT);
        }
        // atualizar o tamanho da Matriz A
        /*explicaçao
        cria uma variavel de recuperação dos valores anteriores e os seta
        remove as textbox dos resultados anteriores
        chama a funçao para gerar os textbox*/
        private void ChangeLengthA(object sender, EventArgs e)
        {
            TextBox[,] MT = MA;
            foreach (TextBox text in MA)
            {
                tabPage1.Controls.Remove(text);
            }
            ReloadA(MT);

        }
        // atualizar o tamanho da Matriz B
        private void ChangeLengthB(object sender, EventArgs e)
        {
            TextBox[,] MT = MB;
            foreach (TextBox text in MB)
            {
                tabPage1.Controls.Remove(text);
            }
            ReloadB(MT);

        }
        // atualizar a operação matemática atual
        private void ChangeOperation(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            Operation.Text = clicked.Text;
        }
        // resolver as operações 
        // explicação ao longo
        private void Solution(object sender, EventArgs e)
        {
            // verifica se for soma/subtraçao
            if (Operation.Text.Equals("+") || Operation.Text.Equals("-"))
            {
                //verifica se é possivel fazer a conta
                if (M1C.Value.Equals(M2C.Value) && M1L.Value.Equals(M2L.Value))
                {
                    // seta o tamanho da matriz do resultado e carrega as textbox
                    M3C.Text = M1C.Value.ToString();
                    M3L.Text = M1L.Value.ToString();
                    ReloadC();
                    try
                    {
                        //soma ou subtrai os valores de cada bloco da matriz e os coloca na textbox certa.
                        for (int i = 0; i < int.Parse(M3C.Text); i++)
                        {
                            for (int j = 0; j < int.Parse(M3L.Text); j++)
                            {
                                if (Operation.Text.Equals("+"))
                                    MC[i, j].Text = (double.Parse(MA[i, j].Text) + double.Parse(MB[i, j].Text)).ToString();
                                else
                                    MC[i, j].Text = (double.Parse(MA[i, j].Text) - double.Parse(MB[i, j].Text)).ToString();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Não deixe caixas vazias e insira somente números.");
                    }
                }
                else
                    MessageBox.Show("Necessário que as duas matrizes sejam de mesmo tamanho para ocorrer essa operação.");
            }
            //verifica se é uma multiplicação
            else if (Operation.Text.Equals("X"))
            {
                //verifica se é possivel fazer a conta
                if (M1C.Value.Equals(M2L.Value))
                {
                    // seta o tamanho da matriz do resultado e carrega as textbox
                    M3L.Text = M1L.Value.ToString();
                    M3C.Text = M2C.Value.ToString();
                    ReloadC();
                    try
                    {
                        // variavel onde fica armazenado o resultado antes de colocar na matriz
                        double[,] C = new double[int.Parse(M2C.Value.ToString()), int.Parse(M1L.Value.ToString())]; 
                        //solução para o problema da multiplicacao não pegar todos os valores
                        // faz a logica para executar uma multiplicaçao de matrizes
                        if (M1L.Value == M2C.Value)
                        {
                            for (int i = 0; i < M1L.Value; i++)
                            {
                                for (int j = 0; j < M2C.Value; j++)
                                {
                                    for (int k = 0; k < M2L.Value; k++)
                                    {
                                        C[i, j] += double.Parse(MA[k, j].Text) * double.Parse(MB[i, k].Text);
                                        MC[i, j].Text = C[i, j].ToString();
                                    }
                                }
                            }
                        }
                        else if (M1L.Value > M2C.Value)
                        {
                            for (int i = 0; i < M1L.Value; i++)
                            {
                                for (int j = 0; j < M1L.Value; j++)
                                {
                                    for (int k = 0; k < M2L.Value; k++)
                                    {
                                        C[i, j] += double.Parse(MA[k, j].Text) * double.Parse(MB[i, k].Text);
                                        MC[i, j].Text = C[i, j].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < M2C.Value; i++)
                            {
                                for (int j = 0; j < M1L.Value; j++)
                                {
                                    for (int k = 0; k < M2L.Value; k++)
                                    {
                                        C[i, j] += double.Parse(MA[k, j].Text) * double.Parse(MB[i, k].Text);
                                        MC[i, j].Text = C[i, j].ToString();
                                    }
                                }
                            }
                        }
                        
                    }
                    catch
                    {
                        MessageBox.Show("Não deixe caixas vazias e insira somente números.");
                    }

                }
                else
                    MessageBox.Show("Número de colunas da Matriz A deve ser igual ao número de linhas da Matriz B");
            }
        }
        //transpor (inverter) a matriz
        /*explicaçao
         verifica qual das matrizes será invertida
         salva os valores anteriores em ordem contraria
         inverte o tamanho das matrizes
         remove os TextBox e os recria adicionando os valores ja invertidos*/
        private void Transpor(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked.Name.Equals("A"))
            {
                string[,] MT = new string[int.Parse(M1L.Value.ToString()), int.Parse(M1C.Value.ToString())];
                for (int i = 0; i < M1L.Value; i++)
                {
                    for (int j = 0; j < M1C.Value; j++)
                    {
                        MT[i, j] = MA[j, i].Text;
                    }
                }
                decimal MTL = M1L.Value;
                M1L.Value = M1C.Value;
                M1C.Value = MTL;
                foreach (TextBox text in MA)
                {
                    tabPage1.Controls.Remove(text);
                }
                ReloadA(null);
                for (int i = 0; i < M1C.Value; i++)
                {
                    for (int j = 0; j < M1L.Value; j++)
                    {
                        MA[i, j].Text = MT[i, j].ToString();
                    }
                }
            }
            else
            {
                string[,] MT = new string[int.Parse(M2L.Value.ToString()), int.Parse(M2C.Value.ToString())];
                for (int i = 0; i < M2L.Value; i++)
                {
                    for (int j = 0; j < M2C.Value; j++)
                    {
                        MT[i, j] = MB[j, i].Text;
                    }
                }
                decimal MTL = M2L.Value;
                M2L.Value = M2C.Value;
                M2C.Value = MTL;
                foreach (TextBox text in MB)
                {
                    tabPage1.Controls.Remove(text);
                }
                ReloadB(null);
                for (int i = 0; i < M2C.Value; i++)
                {
                    for (int j = 0; j < M2L.Value; j++)
                    {
                        MB[i, j].Text = MT[i, j].ToString();
                    }
                }
            }
        }
        // reiniciar a pagina inicial
        private void Clear(object sender, EventArgs e)
        {
            // seta os tamanhos para 2
            M1L.Value = 2; M2L.Value = 2; M3L.Text = null; M1C.Value = 2; M3C.Text = null;
            // remove todos os textbox
            foreach (TextBox text in MA)
            {
                tabPage1.Controls.Remove(text);
            }
            foreach (TextBox text in MB)
            {
                tabPage1.Controls.Remove(text);
            }
            foreach (TextBox text in MC)
            {
                tabPage1.Controls.Remove(text);
            }
            // recria os padroes e zera o texto da operação
            ReloadA(null);
            ReloadB(null);
            Operation.Text = "";
            
        }
        // desenhar o texto das coordenadas dos pontos
        /*void writeCoordenates(int a, int b)
        {
            Label[] backUp = Coordenadas;
            // recebe as coordenadas, seta a localizacao, adiciona o texto e o desenha na pagina
            Coordenadas = new Label[Coordenadas.Length];
            try
            {
                for (int i = 0; i < backUp.Length; i++)
                {
                    Coordenadas[i] = new Label();
                    Coordenadas[i] = backUp[i];
                    Coordenadas[i].AutoSize = true;
                }
            }
            catch { }
            Coordenadas[Coordenadas.Length - 1] = new Label();
            Coordenadas[Coordenadas.Length - 1].AutoSize = true;
            Coordenadas[Coordenadas.Length - 1].Location = new Point(a * 5 + tabPage5.Width/2, tabPage5.Height - b * 5 - tabPage5.Height/2);
            Coordenadas[Coordenadas.Length - 1].Text = "[" + a + "," + b + "]";
            Coordenadas[Coordenadas.Length - 1].SendToBack();
            tabPage5.Controls.Add(Coordenadas[Coordenadas.Length-1]);
        }*/
        // chamar as funçoes de desenho
        private void draw(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            drawCartesian();
            try
            {
                Color color = Color.Red;
                drawLines(M1C.Value, MA, color);
                Label myLine = legA;
                myLine.Text = "Matriz A";
                myLine.ForeColor = color;
            }
            catch
            {
                Label myLine = legA;
                myLine.Text = "Matriz A cant load";
                myLine.ForeColor = Color.Black;
            }
            try 
            {
                Color color = Color.Blue;
                drawLines(M2C.Value, MB,color); 
                Label myLine = legB;
                myLine.Text = "Matriz B";
                myLine.ForeColor = color;
            }
            catch
            {
                Label myLine = legB;
                myLine.Text = "Matriz B cant load";
                myLine.ForeColor = Color.Black;
            }
            try
            {
                Color color = Color.Green;
                drawLines(decimal.Parse(M3C.Text), MC, color);
                Label myLine = legC;
                myLine.Text = "Matriz C";
                myLine.ForeColor = color;
            }
            catch
            {
                Label myLine = legC;
                myLine.Text = "Matriz C cant load";
                myLine.ForeColor = Color.Black;
            }
            try
            {
                Color color = Color.Pink;
                drawLines(M1C_.Value, MO,color);
                Label myLine = legO;
                myLine.Text = "Matriz O";
                myLine.ForeColor = color;
            }
            catch
            {
                Label myLine = legO;
                myLine.Text = "Matriz O cant load";
                myLine.ForeColor = Color.Black;
            }
            try
            {
                Color color = Color.PowderBlue;
                drawLines(decimal.Parse(M3C_.Text), MF,color);
                Label myLine = legF;
                myLine.Text = "Matriz F";
                myLine.ForeColor = color;
            }
            catch
            {
                Label myLine = legF;
                myLine.Text = "Matriz F cant load";
                myLine.ForeColor = Color.Black;
            }
            
        }
        // desenhar linhas da Matriz
        /*explicaçao
         pega a caneta e seta sua cor
         chama a funçao de desenhar linha, pega os valores do ponto atual e do proximo ponto, invertendo o eixo y
         desenha coordenadas nele*/
        void drawLines(decimal MCValue, TextBox[,] M, Color color)
        {
            myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            myPen.Color = color;
            int multiply = 5;
            for (int i = 0; i < MCValue - 1; i++)
            {
                formGraphics.DrawLine(myPen, int.Parse(M[i, 0].Text) * multiply + tabPage5.Width / 2, tabPage5.Height - int.Parse(M[i, 1].Text) * multiply - tabPage5.Height/2,
                    int.Parse(M[i + 1, 0].Text) * multiply + tabPage5.Width / 2, tabPage5.Height - int.Parse(M[i + 1, 1].Text) * multiply - tabPage5.Height/2);
            }
            // esse é separado pois tem que pegar o primeiro ponto
            formGraphics.DrawLine(myPen, int.Parse(M[int.Parse(MCValue.ToString()) - 1, 0].Text) * multiply + tabPage5.Width / 2, tabPage5.Height - int.Parse(M[int.Parse(MCValue.ToString()) - 1, 1].Text) * multiply- tabPage5.Height/2,
                int.Parse(M[0, 0].Text) * multiply + tabPage5.Width / 2, tabPage5.Height - int.Parse(M[0, 1].Text) * multiply- tabPage5.Height/2);
        }
        void drawCartesian()
        {
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            formGraphics.DrawLine(myPen, 0, tabPage5.Height / 2, tabPage5.Width, tabPage5.Height / 2);
            formGraphics.DrawLine(myPen, tabPage5.Width / 2, 0, tabPage5.Width / 2, tabPage5.Height);
        }
        // quando troca de tela
        /*explicacao
         limpa a tela desenho
         remove todas as textbox
         atualiza as textbox para ficarem do mesmo tamanho nas telas*/
        private void Clear(object sender, TabControlEventArgs e)
        {
            tabPage5.Controls.Clear();
            legA.Text = "...";
            legA.ForeColor = Color.Black;
            legB.Text = "...";
            legB.ForeColor = Color.Black;
            legC.Text = "...";
            legC.ForeColor = Color.Black;
            legO.Text = "...";
            legO.ForeColor = Color.Black;
            legF.Text = "...";
            legF.ForeColor = Color.Black;
        }
        // soluções em uma matriz
        private void SolutionOther(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked.Text.Equals("X"))
            {
                try
                {
                    // seta o tamanho da matriz do resultado e carrega as textbox
                    M3C_.Text = M1C_.Value.ToString();
                    M3L_.Text = M1L_.Value.ToString();
                    ReloadF();
                    //multiplica cada bloco da matriz e os coloca na textbox certa.
                    for (int i = 0; i < int.Parse(M3C_.Text); i++)
                    {
                        for (int j = 0; j < int.Parse(M3L_.Text); j++)
                        {
                            if (value.Text.Equals("") || value.Text.Equals(null))
                                MF[i, j].Text = (double.Parse(MO[i, j].Text) * 0).ToString();
                            else
                                MF[i, j].Text = (double.Parse(MO[i, j].Text) * double.Parse(value.Text)).ToString();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Não deixe caixas vazias e insira somente números.");
                }
            }
            else if (clicked.Text.Equals("Det"))
            {
                // verifica se é quadrada
                if (M1L_.Value.Equals(M1C_.Value))
                {
                    //cria uma matriz e seta os valores para serem a da matriz que usaremos
                    double[,] MT = new double[int.Parse(M1C_.Text), int.Parse(M1L_.Text)];
                    for (int i = 0; i < int.Parse(M1C_.Text); i++)
                    {
                        for (int j = 0; j < int.Parse(M1L_.Text); j++)
                        {
                            MT[i, j] = double.Parse(MO[i, j].Text);
                        }
                    }
                    // escreve na caixa de texto o retorno da det
                    value.Text = DET(MT,int.Parse(M1C_.Value.ToString())).ToString();
                }
                else
                    MessageBox.Show("Matriz deve ser quadrada");
            }
            else if (clicked.Text.Equals("Inver"))
            {
                //verifica se é quadrada   
                if (M1L_.Value.Equals(M1C_.Value))
                {
                    //cria uma matriz e seta os valores para serem a da matriz que usaremos
                    double[,] MT = new double[int.Parse(M1C_.Text), int.Parse(M1L_.Text)];
                    for (int i = 0; i < int.Parse(M1C_.Text); i++)
                    {
                        for (int j = 0; j < int.Parse(M1L_.Text); j++)
                        {
                            MT[i, j] = double.Parse(MO[i, j].Text);
                        }
                    }
                    //pega o determinante da matriz e colona na caixa de texto
                    double det = DET(MT, int.Parse(M1C_.Value.ToString()));
                    value.Text = det.ToString();
                    // verifica se é inversivel
                    if (det != 0)
                    {
                        // seta o tamanho da matriz do resultado
                        M3C_.Text = M1C_.Value.ToString();
                        M3L_.Text = M1L_.Value.ToString();
                        ReloadF();
                        // pega a inversa e coloca em cada caixa de texto
                        MT = Invert(det,MT,int.Parse(M3C_.Text));
                        for (int i = 0; i < int.Parse(M3C_.Text); i++)
                        {
                            for (int j = 0; j < int.Parse(M3L_.Text); j++)
                            {
                                MF[i, j].Text = MT[i, j].ToString();
                            }
                        }
                    }
                    else
                        MessageBox.Show("Essa Matriz é Singular");
                }
                else
                    MessageBox.Show("Matriz deve ser quadrada");

            }
        }
        // determinante
        double DET(double[,] mat, int ord)
        {
            // pede uma matriz e seu tamanho
            // se ela for 2x2, retorna do jeito simples
            double myDet = 0;
            if (ord.Equals(2))
                return (mat[0, 0] * mat[1, 1] - mat[1, 0] * mat[0, 1]);
            // se ela for 1x1, retorna ela msm
            else if (ord.Equals(1))
                return (mat[0, 0]);
            else
            {
                double[,] matAux = new double[ord - 1, ord - 1];
                int colAux = 0;

                for (int i = 0; i < ord; i++)
                {

                    for (int linha = 1; linha < ord; linha++)
                    {
                        for (int coluna = 0; coluna < ord; coluna++)
                        {
                            if (i != coluna)
                                matAux[linha - 1, colAux++] = mat[linha, coluna];
                        }

                        colAux = 0;
                    }

                    if (mat[0, i] != 0)
                        myDet += (int)Math.Pow((-1), i) * mat[0, i] * DET(matAux, ord - 1);
                }
            }
            return (myDet);
        }
        // inversa
        double[,] Invert(double det, double[,] mat, int ord)
        {
            // pede o det de uma matriz, essa matriz e seu tamanho
            // o valor a ser multiplicado, matriz cofator dessa matriz, matriz adjunta e matriz inversa
            double FirstValue = 1 / double.Parse(det.ToString());
            double[,] cof = Cofatores(mat, ord);
            double[,] Adjunt = cof;
            double[,] Inver = Adjunt;
            // seta q a matriz adjunta é a transposta da cofator
            // seta q a inversa é o produto do valor a ser multiplicado com a adjunta
            for (int i = 0; i < ord; i++)
            {
                for (int j = 0; j < ord; j++)
                {
                    Adjunt[i, j] = cof[j, i];
                    Inver[i, j] = FirstValue * Adjunt[i, j];
                }
            }
            return Inver;
        }
        // matriz de cofatores
        double[,] Cofatores(double[,] mat, int ord)
        {
            // pede uma matriz e seu tamanho
            // determina uma matriz auxiliar com o mesmo tamanho
            double[,] matAux = new double[ord, ord];  
            for (int linha = 0; linha < ord; linha++)
            {
                for (int coluna = 0; coluna < ord; coluna++)
                {
                    //para cada valor da matriz original, cria-se uma matriz igual, excluindo-se todos os valores da linha e coluna desse valor.
                    double[,] sub = new double[ord - 1, ord - 1];
                    int C = 0;
                    int L = 0;
                    for (int i = 0; i < ord; i++)
                    {
                        if (i.Equals(linha)) continue;
                        for (int j = 0; j < ord; j++)
                        {
                            if (j.Equals(coluna)) continue;
                            sub[L, C] = mat[i, j];
                            C++;
                        }
                        L++;
                        C = 0;
                    }
                    // determina que no lugar daqle valor fica o determinante da matriz criada pra ele, multiplicada por -1 elevado a soma da linha e coluna dele
                    matAux[linha,coluna] = Math.Pow(-1,linha+coluna)*DET(sub, ord - 1);

                }
            }
            return matAux;
        }

    }


}
