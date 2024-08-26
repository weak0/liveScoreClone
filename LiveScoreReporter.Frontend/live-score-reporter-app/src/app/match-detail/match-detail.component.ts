import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService, MatchEvent } from '../event.service';
import { MatchService, Match } from '../match.service';
import { faFutbol, faExclamationTriangle, faExchangeAlt, faBook } from '@fortawesome/free-solid-svg-icons';
import { SignalRService } from '../signalr.service';

@Component({
  selector: 'app-match-detail',
  templateUrl: './match-detail.component.html',
  styleUrls: ['./match-detail.component.css']
})
export class MatchDetailComponent implements OnInit {
  gameId!: number;
  events: MatchEvent[] = [];
  match!: Match;

  faGoal = faFutbol;
  faYellowCard = faExclamationTriangle;
  faSubstitution = faExchangeAlt;
  faOther = faBook;

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService,
    private matchService: MatchService,
    private signalRService: SignalRService
  ) {}

  ngOnInit(): void {
    this.gameId = Number(this.route.snapshot.paramMap.get('id'));
    this.signalRService.startConnection();
    this.signalRService.addEventListener(this); // Przekaż komponent jako argument
    this.refreshData(); // Początkowe załadowanie danych
  }

  loadMatchDetails(): void {
    this.matchService.getMatch(this.gameId).subscribe((match: Match) => {
      this.match = match;
    });
  }

  loadEvents(): void {
    this.eventService.getEvents(this.gameId).subscribe((data: MatchEvent[]) => {
      const newEvents = data.filter(event => !this.events.some(e => e.Time === event.Time && e.Details === event.Details));
      this.events = [...this.events, ...newEvents];
    });
  }

  handleNewEvent(newEvent: MatchEvent): void {
    this.eventService.addNewEvent(newEvent);  // Dodanie nowego wydarzenia do serwisu
    this.events.push(newEvent);  // Dodanie nowego wydarzenia do lokalnej listy komponentu
    this.refreshData(); // Odśwież dane po dodaniu nowego wydarzenia
  }

  refreshData(): void {
    this.loadMatchDetails();
    this.loadEvents();
  }

  
  getEventIcon(eventType: number) {
    switch (eventType) {
      case 0: return this.faGoal; 
      case 1: return this.faYellowCard; 
      case 2: return this.faSubstitution;
      default: return this.faOther; 
    }
  }
  
}
