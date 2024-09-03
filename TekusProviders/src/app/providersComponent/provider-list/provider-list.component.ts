import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { ProvidersTekuService } from '../../services/providers-teku.service';
import { Provider } from '../../models/provider.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-providers-list',
  templateUrl: './providers-list.component.html',
  styleUrls: ['./providers-list.component.css']
})
export class ProvidersListComponent implements OnInit {
  displayedColumns: string[] = ['NIT', 'Name', 'Email', 'IsActive', 'actions'];
  dataSource = new MatTableDataSource<Provider>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private providersService: ProvidersTekuService) { }

  ngOnInit(): void {
    this.loadProviders();
  }

  loadProviders(): void {
    this.providersService.getProvidersTekus().subscribe(
      (data: Provider[]) => {
        this.dataSource.data = data;
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error => console.error('Error fetching providers', error)
    );
  }
}
