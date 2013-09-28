Seesmic Plugin – Video Preview
My second Seesmic plugin is a much more “useable” plugin and, honestly, I’m quite surprised it works as well as it does given how little I know about the Seesmic development kit and my previous experience with the Silverlight WebBrowser control. Anywho, let’s jump right into it, this plugin, the Video Preview plugin, provides the user with timeline access to videos posted from URLs on YouTube (youtube.com and youtu.be) and TwitVid (and now Vimeo!). It will show a small graphic of the first frame and, if the user clicks on the graphic, it will open a dialog within Seesmic and allow the user to play the video in the dialog. To return to the timeline, the user simply closes the dialog. Pretty easy…. and there’s a setting that allows the user to set up the option to automatically show the preview thumbnail of the video.

With version 1.1.1.0, I’ve also added support for migre.me short URLs and slightly enhanced performance. The creation of the thumbnails are run on their thread so there’s no blocking of the app (non-technical terms… my plugin isn’t causing the app to be slow in the case of a large number of video links).

Per version 1.1.2.0, I have added support for all unshort.me supported URL shortening services which include but are not limited to bit.ly, TinyUrl.com, is.gd, cli.gs, etc. So if a Twitvid or YouTube link is wrapped in one of these shortened URLs, the preview will be shown underneath the text/post from which it appeared. For a full list, look here.

Per version 1.2.0.0, I have added support for Vimeo links. Also, I removed the previous code that would still try to individually parse certain URLs in addition to parsing shortened URLs. Now I send everything to the unshort.me URL unshortening service. This takes away a previous user setting to auto process shortened URLs but I have doubts that anyone ever turned that off.

Per 2.2.2.0, I have removed the use of the unshortening service, unshort.me, and all unshortening of URLs are handled within the plugin itself. This is due to instability and potential shutdown of the unshort.me service.

FYI, if you don’t have flash installed for IE or flash is disabled in your IE settings, the videos in the dialogs will not show. Install/Re-enable flash in your IE setting to fix this.

Here’s a screenshot of it in action:



So here’s the info to download and install the Seesmic plugin for Video Preview:

(Download Seesmic Desktop here)

Download the XAP file here.
Save this file in the Seesmic plugins folder at “My Documents\Seesmic\Seesmic Desktop 2\Plugins” – Win7 users can copy and paste this into their run: %HOMEDRIVE%%HOMEPATH%\My Documents\Seesmic\Seesmic Desktop 2\Plugins
Now just run the application and you should see TinyURL show up as an option for shortening service when posting a link
Many, many thanks to Tim Heuer and his plugin work with Seesmic Desktop.

This plugin is hosted on the Seesmic Desktop 2 Marketplace (Thanks Seesmic!)!
