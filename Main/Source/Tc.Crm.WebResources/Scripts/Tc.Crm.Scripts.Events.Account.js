﻿if (typeof (Tc) === "undefined") {
    Tc = {
        __namespace: true
    };
}
if (typeof (Tc.Crm) === "undefined") {
    Tc.Crm = {
        __namespace: true
    };
}
if (typeof (Tc.Crm.Scripts) === "undefined") {
    Tc.Crm.Scripts = {
        __namespace: true
    };
}
if (typeof (Tc.Crm.Scripts.Events) === "undefined") {
    Tc.Crm.Scripts.Events = {
        __namespace: true
    };
}
Tc.Crm.Scripts.Events.Account = (function () {
    "use strict";
    // private stuff

    function onLoad() {

        Tc.Crm.Scripts.Library.Contact.GetNotificationForPhoneNumber("telephone1");
        
    }
    var onChangeTelephone1 = function () {
        Tc.Crm.Scripts.Library.Contact.GetNotificationForPhoneNumber("telephone1");
    }
    var onChangeTelephone2 = function () {
        Tc.Crm.Scripts.Library.Contact.GetNotificationForPhoneNumber("telephone2");
    }
    

    // public methods     
    return {

        OnLoad: function () {
            onLoad();
        },
        OnChangeTelephone1: function () {
            onChangeTelephone1();
        },
        OnChangeTelephone2: function () {
            onChangeTelephone2();
        }

    };
})();// JavaScript source code