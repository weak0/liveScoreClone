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
    this.signalRService.addEventListener(this); 
    this.refreshData(); 
  }

  
  handleNewEvent(newEvent: MatchEvent): void {
    this.refreshData(); 
  }

 
  refreshData(): void {
    console.log('Refreshing data...');
    this.loadMatchDetails(); 
    this.loadEvents();      
  }

 
  loadMatchDetails(): void {
    this.matchService.getMatch(this.gameId).subscribe((match: Match) => {
      this.match = match;
    });
  }

 
  loadEvents(): void {
    this.eventService.getEvents(this.gameId).subscribe((data: MatchEvent[]) => {
      this.events = data; 
    });
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
