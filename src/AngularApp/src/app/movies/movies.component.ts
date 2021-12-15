import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { Observable, Subscription } from 'rxjs';
import { share, tap } from 'rxjs/operators';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MoviesComponent implements OnInit, OnDestroy {
  movies$: Observable<any> | undefined;
  loading = true;
  error: any;
  fetchNumber = 10;

  private subscriptions = new Subscription();

  constructor(
    private changeDetector: ChangeDetectorRef,
    private apollo: Apollo
  ) {}

  ngOnInit(): void {
    this.fetchMovies();
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  fetchMovies(): void {
    this.movies$ = this.apollo
      .watchQuery({
        query: gql`
          {
            movies(limit: ${this.fetchNumber}) {
              title
              cast {
                firstName
                surName
              }
              releaseYear
            }
          }
        `,
      })
      .valueChanges.pipe(
        share(),
        tap((data) => {
          this.changeDetector.detectChanges();
        })
      );
  }
}
