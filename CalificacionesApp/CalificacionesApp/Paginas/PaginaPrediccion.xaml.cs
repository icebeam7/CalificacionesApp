using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalificacionesApp.Modelos;
using CalificacionesApp.Servicios;

namespace CalificacionesApp.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaPrediccion : ContentPage
	{
		public PaginaPrediccion ()
		{
			InitializeComponent ();
		}

        public class StringTable
        {
            public string[] ColumnNames { get; set; }
            public string[,] Values { get; set; }
        }

        async void btnPredecir_Clicked(object sender, EventArgs e)
        {
            var datos = new
            {
                Inputs = new Dictionary<string, StringTable>() {
                        {
                        "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"CollegeGraduate", "CourseA.CourseID", "CourseA.GradeID", "CourseA.PassFailGrade", "CourseA.Semester", "CourseA.YearsSinceHighSchool", "CourseB.CourseID", "CourseB.OverallGPA", "CourseB.PassFailGrade", "CourseB.Semester", "GPADifferenceFromCourseAtoB", "HighSchoolGraduate", "LogTimeBetweenClasses", "NativeEnglishSpeaker", "SameInstructor", "Transition", "CourseA.Instructor", "CourseB.Instructor", "SID", "Student Name"},
                                Values = new string[,] {  { txtGraduadoC.Text, txtCursoA.Text, txtCalifA.Text, txtPassFailA.Text, txtSemestreA.Text, txtAñosA.Text, txtCursoB.Text, txtGPA.Text, "NULL", txtSemestreB.Text, txtDiferencia.Text, txtGraduadoB.Text, txtLogTime.Text, txtIngles.Text, txtInstructor.Text, txtTransicion.Text, txtInstructorA.Text, txtInstructorB.Text, txtID.Text, txtNombre.Text },  }
                            }
                        },
                    },
                GlobalParameters = new Dictionary<string, string>()
                {
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
            var resultado = await ServicioPrediccion.PredecirCalificacion(json);

            await DisplayAlert("¿Aprobado?", resultado, "OK");
        }
    }
}