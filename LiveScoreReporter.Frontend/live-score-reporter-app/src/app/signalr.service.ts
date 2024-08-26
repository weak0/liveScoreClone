import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MatchDetailComponent } from './match-detail/match-detail.component';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  constructor() {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5254/matchHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error while starting SignalR connection: ' + err));
  }

  public addEventListener(component: MatchDetailComponent): void {
    if (this.hubConnection) {
      this.hubConnection.on('ReceiveEventUpdate', (gameId: string, eventData: string) => {
        console.log('Received event for game:', gameId);
        const eventObj = JSON.parse(eventData);

        if (Number(gameId) === component.gameId) {
          console.log('Processing event:', eventObj);
          component.handleNewEvent(eventObj); // Wywołanie metody handleNewEvent w komponencie
        }
      });
    } else {
      console.error('Hub connection is not established.');
    }
  }
}
