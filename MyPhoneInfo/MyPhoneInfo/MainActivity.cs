using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using MyPhoneInfo.Fragments;

namespace MyPhoneInfo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_camera);
                navigationView.Menu.PerformIdentifierAction(Resource.Id.nav_camera, MenuPerformFlags.None);
            }
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                if (this.FragmentManager.BackStackEntryCount == 1)
                {
                    base.OnBackPressed();
                    base.OnBackPressed();
                }

                if (this.FragmentManager.BackStackEntryCount > 1)
                {
                    String title = this.FragmentManager.GetBackStackEntryAt(this.FragmentManager.BackStackEntryCount - 2).Name;
                    int pos = int.Parse(title);

                    NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                    navigationView.SetCheckedItem(pos);
                    this.Title = navigationView.Menu.FindItem(pos).ToString();

                }

                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                this.Title = item.ToString();
                // Create a new fragment and a transaction.
                FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                MyPhoneInfo.Fragments.System aDifferentDetailsFrag = new MyPhoneInfo.Fragments.System();

                // The fragment will have the ID of Resource.Id.fragment_container.
                fragmentTx.Replace(Resource.Id.frameLayout1, aDifferentDetailsFrag);

                // Add the transaction to the back stack.
                fragmentTx.AddToBackStack(id.ToString());

                fragmentTx.SetTransition(FragmentTransit.FragmentFade);
                // Commit the transaction.
                fragmentTx.Commit();
            }
            else if (id == Resource.Id.nav_gallery)
            {
                this.Title = item.ToString();
                // Create a new fragment and a transaction.
                FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                CPU aDifferentDetailsFrag = new CPU();

                // The fragment will have the ID of Resource.Id.fragment_container.
                fragmentTx.Replace(Resource.Id.frameLayout1, aDifferentDetailsFrag);

                // Add the transaction to the back stack.
                fragmentTx.AddToBackStack(id.ToString());

                fragmentTx.SetTransition(FragmentTransit.FragmentFade);
                // Commit the transaction.
                fragmentTx.Commit();
            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

