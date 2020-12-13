# Lightning Alert
This is a just a simple console app for lightning alert.
To use this app, provide the file full path and hit Enter. See example below.

***
Input the full path of the lightning file:
C:\lightning.json

*\<lightning alerts will show here\>*
****


### Questions:
-   What is the  [time complexity](https://en.wikipedia.org/wiki/Time_complexity)  for determining if a strike has occurred for a particular asset?

> 	 O(n) - mainly for the alert tracker.

-   If we put this code into production, but found it too slow, or it needed to scale to many more users or more frequent strikes, what are the first things you would think of to speed it up?

> I would make this app listen to a MessageQueue instead, then create a separate console app where the users can upload the files and the data will be send to the queue. In that way, even if multiple users upload large files at the same time, Lightning Alert app will not gets overwhelmed. Also, it might be a good idea to move the assets to a database so that we don't need to restart the app when there's an assets that's need to be add/update/delete.
