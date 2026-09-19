# PongDBGA



Il progetto è stato iniziato dalla costruzione della scena mettendo le cose necessarie per fare PONG, quindi i player, la palla, il bordi e il punteggio nella UI. Queste cose sono state implementati usando sprite e collider2D visto che usando asset 3D per un gioco in 2D l'ho trovato evitabile.

Dopo averli implementati queste cose sono partito dalla logica del movimento della palla e il rimbalzo sui collider. Per il rimbalzo ho voluto fare una fisica personallizato e non built-in da Unity, quindi la palla muove ad una velocità costante e ogni volta che va in contatto con un collider prende la normale del punto di contatto e calcola una nuova direzione specchiata in base alla normale.

Il prossimo passo è stato quello di mettere gli input dei giocatori usando gli Input Actions e controllandoli da un InputManager che è una classe non MonoBehaviour. Da quel punto ho creato una nuova script per controllare il movimento dei due giocatori senza farli andare oltre i bordi del livello.

Con il gameplay creato sono passato ai punti, ho reso i bordi dietro i giocatori come trigger e creato una script che communica con il GameManager di aumentare il punteggio del giocatore 1 o 2 se la palla entra dentro uno di questi due trigger. Insieme alla creazione del GameManager ho fatto pure un UIManager che gestisce tutto quello che serve essere modificato alla UI.

Per rendere il gioco il più fedele possibile all'originale, ho modificato il movimento della palla facendolo muovere più veloce ogni volta che il giocatore lo riflette e farlo rimbalzare diversamente in base al punto di contatto con la padella del giocatore. La palla rimbalze verso l'alto quando becca la parte superiore della padella, in baso quando becca la parte inferior e dritto quando becca il centro.

Con il gioco creato con il gameplay e sistema di punteggio, tutti gli aspetti necessari per creare PONG sono stati fatti.

Da quel punto ho iniziato ad aggiungere feature opzionali, il primo essendo l'audio, il secondo l'avviamento della partita e pausa e il terzo un game loop con un limite di punti per vincere.



Il tempo impiegato a completare il progetto mi ha impiegato all'incirca 4 ore e mezza per fare il gioco 1:1 all'originale e 2 ore e mezza per le aggiunte opzionali. Quindi la somma di tutto il lavoro equivale a 7 ore totali.



Commandi non specificati in Build:

W/S 			| Movimento Giocatore 1 / Modifica Punteggio per vincere

Freccia Su/Freccia Giù 	| Movimento Giocatore 2 / Modifica Punteggio per vincere

Esc			| Pausa Gioco

R			| Ricarica Scena

