# mh4edit
![demo1](https://raw.githubusercontent.com/Rokumaehn/mh4edit/refs/heads/main/doc/EquipmentBox.png)
![demo2](https://raw.githubusercontent.com/Rokumaehn/mh4edit/refs/heads/main/doc/LbgMagazineSizes.png)
## Introduction
A savegame editor for Monster Hunter 4 Ultimate for the 3ds.
It handles the decryption and re-encryption of savegames, so no need to do that manually.
Currently there is an item box editor, an equipment box editor, a WIP guild quest editor and a WIP Palico editor.
## Things to note
- Currently it's best to only edit existing guild quests and to only edit the second monster if the quest already has a second monster!
- With the equipment editor you can edit relic specific fields on armors that are not relics (this depends on the item's ID). You should not do that, as it will yield an invalid item which will be removed by the game on the next launch.
- For weapons, only the end-game IDs (the IDs which i know are relics) have an unlocked UI other IDs have a reduced UI, so it does not yield an invalid item.
## Known Bugs
- The waist armor ids are off at some point
## Things needed
- A list of all armors and weapons that are relics (Id, Name). So that the editor can properly show or lock the editing of relic-specific settings.
## HowTo
- Backup your save with checkpoint or savedatafiler. You will end up with a system-file and either, some or all of user1, user2, user3 files depending on your characters
- Copy a userX-file to the PC (best use ftpd, so you don't need to remove the card) (It's also better to copy the whole save-folder with all files and rename it, so you have a backup in case something goes wrong)
- Open the userX-file in editor, edit something and hit save. The file will be overwritten
- Move the userX-file (or save-folder) back to the 3ds and import with checkpoint / savedatafiler
- Note: If you move/copy one file to a different folder on your 3ds, make sure that you also copy the other files to the same location otherwise you might loose characters or settings
