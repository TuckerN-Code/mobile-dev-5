using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoQuiz
{
    public class Questions
    {
        public List<string> questions = new List<string>();
        public List<bool> answers = new List<bool>();
        public Questions()
        {
            Context context = Android.App.Application.Context;
            questions.Add(context.Resources.GetText(Resource.String.question_australia));
            answers.Add(true);

            questions.Add(context.Resources.GetText(Resource.String.question_oceans));
            answers.Add(true);

            questions.Add(context.Resources.GetText(Resource.String.question_mideast));
            answers.Add(false);

            questions.Add(context.Resources.GetText(Resource.String.question_africa));
            answers.Add(false);

            questions.Add(context.Resources.GetText(Resource.String.question_americas));
            answers.Add(true);

            questions.Add(context.Resources.GetText(Resource.String.question_asia));
            answers.Add(true);
        }
    }
}