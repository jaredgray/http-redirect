# httpserver-redirect

this is a .net core application that can redirect an incoming url to a local address. this came about so that it would be possible to redirect from an oauth2 provider back to the local machine.

## to set this up on windows - other platforms will be similar

1. install the .net core hosting for windows (or flavor of platform)
2. point a domain to 127.0.0.1 in the hosts file
    ie: 127.0.0.1     pdxcodebits.com
3. build and deploy 
4. spin up a site on port 80 pointing at the deploy
5. add bindings for sites you want to support
    ie: site->edit bindings. add pdxcodebits.com

After the above steps are complete, to configure this app, 
modify the applicationsettings.json and add your own mappings.
the configuration should be self explanitory - file a bug if not.
