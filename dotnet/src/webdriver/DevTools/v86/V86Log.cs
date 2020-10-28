﻿using OpenQA.Selenium.DevTools.V86.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.DevTools.V86
{
    public class V86Log : ILog
    {
        private LogAdapter adapter;

        public V86Log(LogAdapter adapter)
        {
            this.adapter = adapter;
            this.adapter.EntryAdded += OnAdapterEntryAdded;
        }

        public event EventHandler<EntryAddedEventArgs> EntryAdded;

        public async Task Clear()
        {
            await adapter.Clear();
        }

        public async Task Enable()
        {
            await adapter.Enable();
        }

        private void OnAdapterEntryAdded(object sender, Log.EntryAddedEventArgs e)
        {
            if (this.EntryAdded != null)
            {
                EntryAddedEventArgs propagated = new EntryAddedEventArgs();
                propagated.Entry = new LogEntry();
                propagated.Entry.Kind = e.Entry.Source.ToString();
                propagated.Entry.Message = e.Entry.Text;
                this.EntryAdded(this, propagated);
            }
        }
    }
}
