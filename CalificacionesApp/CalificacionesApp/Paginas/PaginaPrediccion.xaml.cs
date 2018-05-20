using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalificacionesApp.Servicios;
using CalificacionesApp.Modelos;

namespace CalificacionesApp.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaPrediccion : ContentPage
	{
		public PaginaPrediccion ()
		{
			InitializeComponent ();
		}

        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }

        async void btnPredecir_Clicked(object sender, EventArgs e)
        {
            Loading(true);

            var id = txtID.Text;
            var nombre = txtNombre.Text;
            var logTime = txtLogTime.Text;
            var ingles = swtIngles.IsToggled ? "Yes" : "No";
            var graduadoC = swtGraduadoC.IsToggled ? "Yes" : "No";
            var gpa = txtGPA.Text;
            var diferencia = txtDiferencia.Text;
            var graduadoB = swtGraduadoB.IsToggled ? "Y" : "N";

            var cursoA = txtCursoA.Text;
            var califA = pckCalifA.SelectedItem.ToString();
            var passFailA = pckPassFailA.SelectedItem.ToString();
            var semestreA = pckSemestreA.SelectedItem.ToString();
            var añosA = txtAñosA.Text;
            var instructorA = txtInstructorA.Text;

            var cursoB = txtCursoB.Text;
            var semestreB = pckSemestreB.SelectedItem.ToString();
            var instructorB = txtInstructorB.Text;

            var transicion = $"{cursoA} TO {cursoB}";
            var instructor = (instructorA == instructorB ? "Same" : "Different") + " Instructor";

            var datos = new
            {
                Inputs = new Dictionary<string, StringTable>()
                {
                    {
                        "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"CollegeGraduate", "CourseA.CourseID", "CourseA.GradeID", "CourseA.PassFailGrade", "CourseA.Semester", "CourseA.YearsSinceHighSchool", "CourseB.CourseID", "CourseB.OverallGPA", "CourseB.PassFailGrade", "CourseB.Semester", "GPADifferenceFromCourseAtoB", "HighSchoolGraduate", "LogTimeBetweenClasses", "NativeEnglishSpeaker", "SameInstructor", "Transition", "CourseA.Instructor", "CourseB.Instructor", "SID", "Student Name"},
                                Values = new string[,] {  { graduadoC, cursoA, califA, passFailA, semestreA, añosA, cursoB, gpa, "NULL", semestreB, diferencia, graduadoB, logTime, ingles, instructor, transicion, instructorA, instructorB, id, nombre},  }
                            }
                    },
                },
                GlobalParameters = new Dictionary<string, string>()
                {
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
            var resultado = await ServicioPrediccion.PredecirCalificacion(json);

            await DisplayAlert("¿Aprobado o En Riesgo?", resultado, "OK");

            Loading(false);
        }
    }
}