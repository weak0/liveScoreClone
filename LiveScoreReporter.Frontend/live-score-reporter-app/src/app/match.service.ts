import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Match {
  GameId: number;
  HomeTeamName: string;
  HomeTeamLogo: string;
  AwayTeamName: string;
  AwayTeamLogo: string;
  GameResult: string;
}

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  private apiUrl = 'http://localhost:5254/games/all'; 

  constructor(private http: HttpClient) { }

  getMatches(): Observable<Match[]> {
    return this.http.get<Match[]>(this.apiUrl);
  }
}
