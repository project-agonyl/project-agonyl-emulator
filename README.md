Project Agonyl - An emulator for A3 MMORPG
==========================================

Overview of A3
---------------
A3 - Art, Alive, Attraction is a Korean MMORPG released around 2003. English version of it was officially released in India in 2005 under the banner of Sify. Later due to some issues it was shut down. But the love for the game did not end there. Thanks to RaGEZONE Forum (http://forum.ragezone.com/f98/) some Indians were able to create A3 private server using the files released there. The released files were just executables compiled by the Chinese developers and the source of these executables were never released.

This is an effort to create an open source emulator for A3. If you are developer with a passion for MMORPG you are welcome to help in its development.

Requirements
------------
1. Dot Net 4.0
2. MySQL

Running the Project
-------------------
1. Build the project using Visual Studio 2017 or higher
2. Create MySQL database
3. Import database schema from ``data/db.sql`` into your emulator database
4. Import example test data from ``data/seed.sql``
5. Update {project-folder}\bin\Debug\LoginServer\LoginServer.ini with proper credentials
8. Run {project-folder}\bin\Debug\LoginServer\LoginServer.exe to start the login server

Currently available features
----------------------------
1. Account validation and login
2. Showing of server details

Client
------
Emulator has been found to be working only with A3 client version 562 (Get local IP hexed client from https://github.com/cyberinferno/InfernoEmu/tree/master/Important%20Files)
