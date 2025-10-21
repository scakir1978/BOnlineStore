import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

export interface TimeZoneOption {
  value: string; // IANA time zone id, e.g., "Europe/Istanbul"
  text: string; // Label like "(GMT+03:00) Europe/Istanbul"
  offsetMinutes: number; // Current UTC offset in minutes
}

@Injectable({ providedIn: 'root' })
export class TimezoneService {
  private readonly fallbackUrl = 'assets/json/iana-timezones.json';

  constructor(private http: HttpClient) {}

  getTimeZones(): Observable<TimeZoneOption[]> {
    const supportsIntlList =
      typeof (Intl as any).supportedValuesOf === 'function';

    if (supportsIntlList) {
      try {
        const ids: string[] = (Intl as any).supportedValuesOf('timeZone');
        const options = ids
          .map((id) => this.buildOption(id))
          .sort((a, b) =>
            a.offsetMinutes === b.offsetMinutes
              ? a.value.localeCompare(b.value)
              : a.offsetMinutes - b.offsetMinutes
          );
        return of(options);
      } catch {
        // fall through to HTTP fallback
      }
    }

    return this.http.get<string[]>(this.fallbackUrl).pipe(
      map((ids) =>
        ids
          .map((id) => this.buildOption(id))
          .sort((a, b) =>
            a.offsetMinutes === b.offsetMinutes
              ? a.value.localeCompare(b.value)
              : a.offsetMinutes - b.offsetMinutes
          )
      ),
      catchError(() => of([]))
    );
  }

  private buildOption(id: string): TimeZoneOption {
    const offsetMinutes = this.getOffsetMinutes(id);
    const label = `(${this.formatOffset(offsetMinutes)}) ${id}`;
    return { value: id, text: label, offsetMinutes };
  }

  private formatOffset(totalMinutes: number): string {
    const sign = totalMinutes >= 0 ? '+' : '-';
    const abs = Math.abs(totalMinutes);
    const hh = Math.floor(abs / 60)
      .toString()
      .padStart(2, '0');
    const mm = (abs % 60).toString().padStart(2, '0');
    return `GMT${sign}${hh}:${mm}`;
  }

  // Best-effort offset calculation using Intl APIs
  private getOffsetMinutes(timeZone: string): number {
    const now = new Date();

    // Try to use formatToParts to extract shortOffset (modern browsers)
    try {
      const parts = new Intl.DateTimeFormat('en-US', {
        timeZone,
        hour12: false,
        timeZoneName: 'shortOffset' as any,
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
      } as any).formatToParts(now);
      const tzName = parts.find((p) => p.type === 'timeZoneName')?.value;
      const m = tzName && tzName.match(/GMT([+-])(\d{1,2})(?::(\d{2}))?/);
      if (m) {
        const sign = m[1] === '-' ? -1 : 1;
        const h = parseInt(m[2], 10) || 0;
        const mins = m[3] ? parseInt(m[3], 10) : 0;
        return sign * (h * 60 + mins);
      }
    } catch {
      // ignore
    }

    // Fallback: compare local UTC time with zoned time by formatting ISO and parsing
    try {
      const local = now.getTime();
      // Format the time in the target timezone and parse back; may be off in some locales but works for en-US
      const zonedStr = now.toLocaleString('en-US', { timeZone });
      const zoned = new Date(zonedStr).getTime();
      const localOffset = now.getTimezoneOffset();
      // Estimate: (zoned - local)/60000 - localOffset
      return -Math.round((zoned - local) / 60000 - localOffset);
    } catch {
      // As a last resort, return 0
      return 0;
    }
  }
}
