using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GeoQuiz.Models;
using Java.Nio.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoQuiz
{
    [Activity(Label = "GameStatsActivity")]
    public class GameStatsActivity : Activity
    {
        Profile profile = new Profile();
        string GAME_STATS = "game_stats";
        string TOTAL_STATS = "total_stats";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.game_stats_main);

            FindViewById<TextView>(Resource.Id.real_name).Text = Intent.GetStringArrayExtra("profile")[0];
            FindViewById<TextView>(Resource.Id.gamer_name).Text = Intent.GetStringArrayExtra("profile")[1];

            FindViewById<TextView>(Resource.Id.c_num_c).Text = Intent.GetIntArrayExtra(GAME_STATS)[0].ToString();
            FindViewById<TextView>(Resource.Id.c_num_i).Text = Intent.GetIntArrayExtra(GAME_STATS)[1].ToString();
            if (Intent.GetIntArrayExtra(GAME_STATS).Sum() > 0)
            {
                double per = (double)Intent.GetIntArrayExtra(GAME_STATS)[0] / (Intent.GetIntArrayExtra(GAME_STATS)[0] + Intent.GetIntArrayExtra(GAME_STATS)[1]) * 100;
                FindViewById<TextView>(Resource.Id.c_p_c).Text = Math.Truncate(per).ToString() + "%";

            }
            else
                FindViewById<TextView>(Resource.Id.c_p_c).Text = "0%";


            FindViewById<TextView>(Resource.Id.t_num_c).Text = Intent.GetIntArrayExtra(TOTAL_STATS)[0].ToString();
            FindViewById<TextView>(Resource.Id.t_num_i).Text = Intent.GetIntArrayExtra(TOTAL_STATS)[1].ToString();
            if (Intent.GetIntArrayExtra(TOTAL_STATS).Sum() > 0)
            {
                double per = (double)Intent.GetIntArrayExtra(TOTAL_STATS)[0] / (Intent.GetIntArrayExtra(TOTAL_STATS)[0] + Intent.GetIntArrayExtra(TOTAL_STATS)[1]) * 100;
                FindViewById<TextView>(Resource.Id.t_p_c).Text = Math.Truncate(per).ToString() + "%";
                TextView iq = FindViewById<TextView>(Resource.Id.IQ);
                iq.Text = Math.Truncate(160 * per / 100).ToString();
                if (per > 70)
                    iq.SetBackgroundColor(Android.Graphics.Color.Green);
                else if (per > 50)
                    iq.SetBackgroundColor(Android.Graphics.Color.Yellow);
                else
                    iq.SetBackgroundColor(Android.Graphics.Color.Red); 
                    
            }
            // Create your application here
        }
    }
}