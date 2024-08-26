import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface MatchEvent {  // Zmieniona nazwa na MatchEvent
  TeamId: number;
  TeamName: string;
  Type: number;
  Details: string;
  Comments: string | null;
  Time: number;
  PlayerName: string;
  AssistPlayerName: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private baseUrl = 'http://localhost:5254/events';
  private events: MatchEvent[] = [];  // Zmieniona nazwa na MatchEvent[]

  constructor(private http: HttpClient) { }

  handleNewEvent(newEvent: MatchEvent): void {  // Zmieniona nazwa na MatchEvent
    this.events.push(newEvent);
  }

  getEvents(gameId: number): Observable<MatchEvent[]> {  
    return this.http.get<MatchEvent[]>(`${this.baseUrl}/${gameId}`);
  }

  addNewEvent(newEvent: MatchEvent): void {  
    this.events.push(newEvent);  // Dodanie nowego wydarzenia do lokalnej listy
  }

  getLocalEvents(): MatchEvent[] {
    return this.events;  // Zwrócenie lokalnej listy wydarzeń
  }
}