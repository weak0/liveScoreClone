import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService, Event } from '../event.service';
import { MatchService, Match } from '../match.service';

@Component({
  selector: 'app-match-detail',
  templateUrl: './match-detail.component.html',
  styleUrls: ['./match-detail.component.css']
})
export class MatchDetailComponent implements OnInit {
  gameId!: number;
  events: Event[] = [];
  match!: Match;

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService,
    private matchService: MatchService
  ) {}

  ngOnInit(): void {
    this.gameId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadMatchDetails();
    this.loadEvents();
  }

  loadMatchDetails(): void {
    this.matchService.getMatch(this.gameId).subscribe((match: Match) => {
      this.match = match;
    });
  }

  loadEvents(): void {
    this.eventService.getEvents(this.gameId).subscribe((data: Event[]) => {
      const newEvents = data.filter(event => !this.events.some(e => e.Time === event.Time && e.Details === event.Details));
      this.events = [...this.events, ...newEvents];
    });
  }

  refreshEvents(): void {
    this.loadEvents();
  }
}
