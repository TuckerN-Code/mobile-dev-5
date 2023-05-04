using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Content;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Android.Util;
using Xamarin.Essentials;
using GeoQuiz.Models;
using Android.Preferences;

namespace GeoQuiz
{

    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int index = 0;
        Questions questions = new Questions();
        const string tag = "Main_Activity";
        string KEY_INDEX = "index";
        string EXTRA_ANSWER_IS_TRUE = "answer_is_true";
        bool isCheater = false;
        string DID_CHEAT = "did_cheat";
        string GAME_STATS = "game_stats";
        string TOTAL_STATS = "total_stats";
        bool isAnswered = false;
        Profile profile = new Profile { firstName = "Henry", gamerName="Badguy"};
        GameStats gameStats, totalStats;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Log.Debug(tag, "OnDestroy Called");
        }

        protected override void OnStop()
        {
            base.OnStop();
            Log.Debug(tag, "OnStop Called");
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            gameStats = new GameStats();
            totalStats = new GameStats();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(BaseContext);
            totalStats.numCorrect = prefs.GetInt("total_correct", 0);
            totalStats.numIncorrect = prefs.GetInt("total_incorrect", 0);
            if (savedInstanceState != null)
            {
                index = savedInstanceState.GetInt(KEY_INDEX);
                isCheater = savedInstanceState.GetBoolean(DID_CHEAT);
                gameStats.numCorrect = savedInstanceState.GetIntArray(GAME_STATS)[0];
                gameStats.numIncorrect = savedInstanceState.GetIntArray(GAME_STATS)[1];
                totalStats.numIncorrect = savedInstanceState.GetIntArray(TOTAL_STATS)[0];
                totalStats.numIncorrect = savedInstanceState.GetIntArray(TOTAL_STATS)[1];
                
            }
            Log.Debug(tag, "OnCreate called");

            Button trueButton = FindViewById<Button>(Resource.Id.true_button);
            Button falseButton = FindViewById<Button>(Resource.Id.false_button);
            TextView question = FindViewById<TextView>(Resource.Id.question_text_view);
            Button nextButton = FindViewById<Button>(Resource.Id.next_button);
            Button cheatButton = FindViewById<Button>(Resource.Id.cheat_button);
            Button statsButton = FindViewById<Button>(Resource.Id.game_stats);

            question.Text = questions.questions[index];

            trueButton.Click += delegate
            {
                if (questions.answers[index])
                {
                    Toast.MakeText(this, Resource.String.correct_toast,
                       ToastLength.Short).Show();
                    gameStats.numCorrect++;
                    totalStats.numCorrect++;
                }
                else
                {
                    Toast.MakeText(this, Resource.String.incorrect_toast,
                        ToastLength.Short).Show();
                    gameStats.numIncorrect++;
                    totalStats.numIncorrect++;
                }
                if (isCheater)
                    Toast.MakeText(this, Resource.String.judgment_toast,
                        ToastLength.Short).Show();
                isAnswered = true;
            };

            falseButton.Click += delegate
            {
                if (!questions.answers[index])
                {
                    Toast.MakeText(this, Resource.String.correct_toast,
                       ToastLength.Short).Show();
                    gameStats.numCorrect++;
                    totalStats.numCorrect++;
                }
                else
                {
                    Toast.MakeText(this, Resource.String.incorrect_toast,
                        ToastLength.Short).Show();
                    gameStats.numIncorrect++;
                    totalStats.numIncorrect++;
                }
                if (isCheater)
                    Toast.MakeText(this, Resource.String.judgment_toast,
                        ToastLength.Short).Show();
                isAnswered = true;
            };

            nextButton.Click += delegate
            {
                index++;
                if (index == questions.questions.Count)
                    index = 0;
                question.Text = questions.questions[index];
                isAnswered = false;
            };

            cheatButton.Click += delegate
            {
                Intent i = new Intent(this, typeof(CheatActivity));
                i.PutExtra(EXTRA_ANSWER_IS_TRUE, questions.answers[index]);
                StartActivityForResult(i, 0);
            };
            statsButton.Click += delegate
            {
                Intent stats = new Intent(this, typeof(GameStatsActivity));
                if (profile != null)
                    stats.PutExtra("profile", new string[] { profile.firstName, profile.gamerName });
                stats.PutExtra("game_stats", new int[2] {gameStats.numCorrect, gameStats.numIncorrect});
                stats.PutExtra("total_stats", new int[2] {totalStats.numCorrect, totalStats.numIncorrect});
                StartActivity(stats);
            };
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            isCheater = data.GetBooleanExtra(DID_CHEAT, false);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[]
            permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode,
               permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            Log.Debug(tag, "OnSavedInstance Called");
            outState.PutInt(KEY_INDEX, index);
            outState.PutBoolean(DID_CHEAT, isCheater);
            outState.PutIntArray(GAME_STATS, new int[2] { gameStats.numCorrect, gameStats.numIncorrect });
            outState.PutIntArray(TOTAL_STATS, new int[2] { totalStats.numCorrect, totalStats.numIncorrect });
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(BaseContext);
            ISharedPreferencesEditor e = prefs.Edit();
            e.PutInt("total_correct", totalStats.numCorrect);
            e.PutInt("total_incorrect", totalStats.numIncorrect);
            e.Apply();
        }

    }

}
