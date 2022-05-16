import { selectHasSetIntervalHappened, selectVisibleStops, setHasSetIntervalHappened, setVisibleStops } from './stopSlice';
import { useAppSelector, useAppDispatch } from '../../app/hooks';
import { fetchUpcomingArrivalsStops1and2 } from './stop-api';
import { Box, Grid, Paper, styled } from '@mui/material';
import { theme } from '../../theme-options';
import { UpcomingArrival } from './data';
import { DateTime } from 'luxon';

function doGetLastestUpcomingArrivals(dispatch: (arg0: any) => void)
{
  fetchUpcomingArrivalsStops1and2()
    .then((visibleStops) => dispatch(setVisibleStops(visibleStops)));
}

function initSetInterval(dispatch: (arg0: any) => void) {
  setInterval(() => {
    doGetLastestUpcomingArrivals(dispatch)
  }, 60000);
}

export function Stop() {
  const dispatch = useAppDispatch();

  const visibleStops = useAppSelector(selectVisibleStops);
  const hasSetIntervalHappened = useAppSelector(selectHasSetIntervalHappened);

  if (visibleStops.length == 0) {
    doGetLastestUpcomingArrivals(dispatch);
  }

  if (!hasSetIntervalHappened) {
    initSetInterval(dispatch);
    dispatch(setHasSetIntervalHappened(true));
  }

  const Item = styled(Paper)(({ theme }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
    ...theme.typography.body2,
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  }));

  var nowTime = DateTime.now();

  return (
    <Grid container spacing={2} className="stop-display-container" padding={10}>
      {
        visibleStops.map(stopInfo => {
          return (
            <Grid item xs={12}>
              <Item>
                <h4>Stop {stopInfo.stopId}</h4>
                <input type="hidden" value={stopInfo.stopId} className="visible-stop"></input>
                {
                  stopInfo.upcomingArrivals.map(arrival => {
                    var arrivalTime = DateTime.fromISO(arrival.arrivalTime, { zone: 'utc' }).toLocal();
                    var waitTimeDiff = arrivalTime.diff(nowTime, ['minutes', 'seconds']);
                    var f = arrivalTime.toString();
                    return (
                      <div>Bus #{arrival.routeId} arriving in {waitTimeDiff.minutes}m {waitTimeDiff.seconds.toFixed(0)}s ({arrivalTime.toFormat('t')})</div>
                    );
                  })
                }
              </Item>
            </Grid>
          )
        })
      }
    </Grid>
  );
}