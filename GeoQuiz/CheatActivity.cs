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
    [Activity(Label = "CheatActivity")]
    public class CheatActivity : Activity
    {
        string EXTRA_ANSWER_IS_TRUE = "answer_is_true";
        string DID_CHEAT = "did_cheat";
        bool answerIsTrue = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.cheat_main);
            TextView ans = FindViewById<TextView>(Resource.Id.answer_text_view);
            Button cheat = FindViewById<Button>(Resource.Id.show_answer_button);

            answerIsTrue = Intent.GetBooleanExtra(EXTRA_ANSWER_IS_TRUE, false);

            Intent result = new Intent(this, typeof(MainActivity));

            cheat.Click += delegate
            {
                ans.Text = answerIsTrue ? "True" : "False";
                result.PutExtra(DID_CHEAT, true);
                SetResult(Result.Ok, result);
            };
            


        }
    }
}