using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace PS4_Tool_v2.Fragments
{
    public class FanControl : Fragment
    {
        private SeekBar temperatureSlider;
        private TextView temperatureText;
        private Button sendBtn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            temperatureSlider = View.FindViewById<SeekBar>(Resource.Id.temperatureSlider);
            temperatureText = View.FindViewById<TextView>(Resource.Id.temeperatureText);
            sendBtn = View.FindViewById<Button>(Resource.Id.sendFanBtn);

            temperatureSlider.ProgressChanged += TemperatureSlider_ProgressChanged;
            sendBtn.Click += SendBtn_Click;
        }

        private void SendBtn_Click(object sender, System.EventArgs e)
        {
            Functions functions = new Functions();
            functions.Send(Context.ApplicationContext, "Fan Payloads/" + "Temp" + temperatureSlider.Progress.ToString() + ".bin");
        }

        private void TemperatureSlider_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            temperatureText.Text = "Temperature: " + temperatureSlider.Progress.ToString();
        }

        public static FanControl NewInstance()
        {
            var frag1 = new FanControl { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fan_control_fragment, container, false);
        }
    }
}