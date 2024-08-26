import { Injectable, NgZone } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MatchDetailComponent } from './match-detail/match-detail.component';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  constructor(private ngZone: NgZone) {}  // Dodanie NgZone

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5254/matchHub') // URL do Twojego SignalR hub
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error while starting SignalR connection: ' + err));
  }

  public addEventListener(component: MatchDetailComponent): void {
    if (this.hubConnection) {
      this.hubConnection.on('ReceiveEventUpdate', (gameId: string, eventData: string) => {
        console.log(`${Number(gameId)} and ${component.gameId}`);
        const eventObj = JSON.parse(eventData);
    
        if (Number(gameId) === component.gameId) {
            console.log('Processing event:', eventObj);
            component.handleNewEvent(eventObj);
        } else {
            console.log('Event does not match current game ID.');
        }
    });
    } else {
      console.error('Hub connection is not established.');
    }
  }
}
