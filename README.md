# PongDBGA



Il progetto è stato iniziato dalla costruzione della scena mettendo le cose necessarie per fare PONG, quindi i player, la palla, il bordi e il punteggio nella UI. Queste cose sono state implementati usando sprite e collider2D visto che usando asset 3D per un gioco in 2D l'ho trovato evitabile. 

Dopo averli implementati queste cose sono partito dalla logica del movimento della palla e il rimbalzo sui collider. Per il rimbalzo ho voluto fare una fisica personallizato e non built-in da Unity, quindi la palla muove ad una velocità costante e ogni volta che va in contatto con un collider prende la normale del punto di contatto e calcola una nuova direzione specchiata in base alla normale.

Il prossimo passo è stato quello di mettere gli input dei giocatori usando gli Input Actions e controllandoli da un InputManager che è una classe non MonoBehaviour. Da quel punto ho creato una nuova script per controllare il movimento dei due giocatori senza farli andare oltre i bordi del livello.

Con il gameplay creato sono passato ai punti, ho reso i bordi dietro i giocatori come trigger e creato una script che communica con il GameManager di aumentare il punteggio del giocatore 1 o 2 se la palla entra dentro uno di questi due trigger. Insieme alla creazione del GameManager ho fatto pure un UIManager che gestisce tutto quello che serve essere modificato alla UI.

Per rendere il gioco il più fedele possibile all'originale, ho modificato il movimento della palla facendolo muovere più veloce ogni volta che il giocatore lo riflette e farlo rimbalzare diversamente in base al punto di contatto con la padella del giocatore. La palla rimbalze verso l'alto quando becca la parte superiore della padella, in baso quando becca la parte inferior e dritto quando becca il centro.

Con il gioco creato con il gameplay e sistema di punteggio, tutti gli aspetti necessari per creare PONG sono stati fatti.

Da quel punto ho iniziato ad aggiungere feature opzionali, il primo essendo l'audio prendendo gli asset da Google e implementadoli nel progetto quando la palla collide ho un punto è stato segnato gestiti da un AudioManager che chiama da un pooler un prefab per avviare il suono. Il secondo è stato di implementare un tasto di avviare la partita invece di farlo avviare automaticamente sull'apertura dell'applicazione e un tasto per mettere in pausa il gioco modificando l'Input, Game e UI Manager per farlo. Il terzo e ultima è stato di implementare un game loop, ovvero scegliere all'inizio un punteggio da arrivare per poter vincere, questa feature l'ho reso modificabile dal giocatore e salvato quando si avvia una partita. Per renderlo possible ho messo altri paneli nella UI e modificato l'Input, Game e UI Manager di nuovo.

Nello svillupo del codice ho provato il più possibile di aggiungere commenti nel mentre che scrivevo il codice ma ho comunque fatto una revisione finale per metterli nei punti in cui mancavano.



Il tempo impiegato a completare il progetto mi ha impiegato all'incirca 4 ore e mezza per fare il gioco 1:1 all'originale e 2 ore e mezza per le aggiunte opzionali. Quindi la somma di tutto il lavoro equivale a 7 ore totali.



Commandi non specificati in Build:

W/S 			| Movimento Giocatore 1 / Modifica Punteggio per vincere

Freccia Su/Freccia Giù 	| Movimento Giocatore 2 / Modifica Punteggio per vincere

Esc			| Pausa Gioco

R			| Ricarica Scena

