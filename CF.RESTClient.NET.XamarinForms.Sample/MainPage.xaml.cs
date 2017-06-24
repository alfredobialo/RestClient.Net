﻿using Atlassian;
using CF.RESTClientDotNet;
using groupkt;
using System;
using System.Text;
using restclientdotnet = CF.RESTClientDotNet;
using System.Threading.Tasks;

namespace CF.RESTClient.NET.Sample
{
    public partial class MainPage
    {
        #region Fields
        private restclientdotnet.RESTClient _BitbucketClient;
        #endregion

        #region Constructror
        public MainPage()
        {
            InitializeComponent();
            AttachEventHandlers();
        }

        private void GetReposButton_Clicked(object sender, EventArgs e)
        {
            GetReposClick();
        }

        #endregion

        #region Private Methods

        private async void GetReposClick()
        {
            try
            {
                ToggleReposBusy(true);

                //Ensure the client is ready to go
                GetBitBucketClient();

                //Download the repository data
                var repos = (await _BitbucketClient.GetAsync<RepositoryList>());

                //Put it in the List Box
                ReposBox.ItemsSource = repos.values;
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            ToggleReposBusy(false);
        }

        private async Task HandleException(Exception ex)
        {
            ErrorModel errorModel = null;

            var rex = ex as RESTException;
            if (rex != null)
            {
                errorModel = rex.Error as ErrorModel;
            }

            string message = $"An error occurred while attempting to use a REST service.\r\nError: {ex.Message}\r\nInner Error: {ex.InnerException?.Message}\r\nInner Inner Error: {ex.InnerException?.InnerException?.Message}";

            if (errorModel != null)
            {
                message += $"\r\n{errorModel.error.message}";
            }

            await DisplayAlert(message);
        }

        #endregion
    }
}
