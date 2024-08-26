import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export interface Event {
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

  constructor(private http: HttpClient) { }

  getEvents(gameId: number): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseUrl}/${gameId}`);
  }
}
