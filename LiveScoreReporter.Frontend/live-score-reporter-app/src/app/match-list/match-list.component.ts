import { Component, OnInit } from '@angular/core';
import { MatchService, Match } from '../match.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-match-list',
  templateUrl: './match-list.component.html',
  styleUrls: ['./match-list.component.css']
})
export class MatchListComponent implements OnInit {

  matches: Match[] = [];

  constructor(private matchService: MatchService, private router: Router) { }

  ngOnInit(): void {
    this.matchService.getMatches().subscribe(data => {
      this.matches = data;
    });
  }

  navigateToMatch(gameId: number): void {
    this.router.navigate(['/match', gameId]);
  }
}
