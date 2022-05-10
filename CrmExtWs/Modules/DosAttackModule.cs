﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Timers;

/// <summary>
/// Block the response to attacking IP addresses.
/// </summary>
namespace CrmExtWs
{
    internal class PreventDosAttackAttribute : Attribute
    {
        private static Dictionary<string, short> _IpAdresses = new Dictionary<string, short>();
        private static Stack<string> _Banned = new Stack<string>();
        private static Timer _Timer = CreateTimer();
        private static Timer _BannedTimer = CreateBanningTimer();

        private const int BANNED_REQUESTS = 10;
        private const int REDUCTION_INTERVAL = 1000; // 1 second
        private const int RELEASE_INTERVAL = 5 * 60 * 1000; // 5 minutes

        private void context_BeginRequest(object sender, EventArgs e)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (_Banned.Contains(ip))
            {
                HttpContext.Current.Response.StatusCode = 403;
                HttpContext.Current.Response.End();
            }

            CheckIpAddress(ip);
        }

        /// <summary>
        /// Checks the requesting IP address in the collection
        /// and bannes the IP if required.
        /// </summary>
        private static void CheckIpAddress(string ip)
        {
            if (!_IpAdresses.ContainsKey(ip))
            {
                _IpAdresses[ip] = 1;
            }
            else if (_IpAdresses[ip] == BANNED_REQUESTS)
            {
                _Banned.Push(ip);
                _IpAdresses.Remove(ip);
            }
            else
            {
                _IpAdresses[ip]++;
            }
        }

        /// <summary>
        /// Creates the timer that substract a request
        /// from the _IpAddress dictionary.
        /// </summary>
        private static Timer CreateTimer()
        {
            Timer timer = GetTimer(REDUCTION_INTERVAL);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            return timer;
        }

        /// <summary>
        /// Creates the timer that removes 1 banned IP address
        /// everytime the timer is elapsed.
        /// </summary>
        /// <returns></returns>
        private static Timer CreateBanningTimer()
        {
            Timer timer = GetTimer(RELEASE_INTERVAL);
            timer.Elapsed += delegate { _Banned.Pop(); };
            return timer;
        }

        /// <summary>
        /// Creates a simple timer instance and starts it.
        /// </summary>
        /// <param name="interval">The interval in milliseconds.</param>
        private static Timer GetTimer(int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Start();

            return timer;
        }

        /// <summary>
        /// Substracts a request from each IP address in the collection.
        /// </summary>
        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (string key in _IpAdresses.Keys)
            {
                _IpAdresses[key]--;
                if (_IpAdresses[key] == 0)
                    _IpAdresses.Remove(key);
            }
        }
    }
}