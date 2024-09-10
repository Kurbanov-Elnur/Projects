import React, { useState } from 'react';
import { format, startOfMonth, endOfMonth, addMonths, subMonths, eachDayOfInterval, isToday } from 'date-fns';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendarDay, faTasks } from '@fortawesome/free-solid-svg-icons';

const tasks = [
    {
        date: '2024-09-01',
        title: 'Meeting with team',
        description: 'Discuss project milestones and next steps.',
        icon: faTasks
    },
    {
        date: '2024-09-02',
        title: 'Client presentation',
        description: 'Prepare slides and gather feedback.',
        icon: faTasks
    },
];

const Calendar = () => {
    const [currentMonth, setCurrentMonth] = useState(new Date());

    const handlePreviousMonth = () => {
        setCurrentMonth(subMonths(currentMonth, 1));
    };

    const handleNextMonth = () => {
        setCurrentMonth(addMonths(currentMonth, 1));
    };

    const startOfMonthDate = startOfMonth(currentMonth);
    const endOfMonthDate = endOfMonth(currentMonth);
    const days = eachDayOfInterval({ start: startOfMonthDate, end: endOfMonthDate });

    const getDayTasks = (date) => {
        const formattedDate = format(date, 'yyyy-MM-dd');
        return tasks.filter(task => task.date === formattedDate);
    };

    return (
        <div className="fixed top-0 right-0 w-72 h-screen bg-[#303841]/60 border border-[#3ec9a7] rounded-md p-4 shadow-lg backdrop-filter backdrop-blur-sm">
            <div className="flex justify-between items-center mb-3">
                <button onClick={handlePreviousMonth} className="text-[#3ec9a7] hover:text-[#2c3e50] text-xl">&lt;</button>
                <h2 className="text-base font-bold">{format(currentMonth, 'MMMM yyyy')}</h2>
                <button onClick={handleNextMonth} className="text-[#3ec9a7] hover:text-[#2c3e50] text-xl">&gt;</button>
            </div>
            <div className="grid grid-cols-7 gap-1 text-xs">
                {['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'].map(day => (
                    <div key={day} className="text-center font-bold">{day}</div>
                ))}
                {days.map(day => (
                    <div
                        key={day.toString()}
                        className={`text-center p-2 cursor-pointer transition-transform transform hover:scale-110 ${isToday(day) ? 'bg-[#3ec9a7] text-white rounded-full' : ''}`}
                    >
                        {day.getDate()}
                    </div>
                ))}
            </div>
            <div className="mt-5">
                {tasks.map((task) => (
                    <a
                        key={task.date}
                        href="#"
                        className="block hover:bg-[#3ec9a7]/10 transition duration-150 ease-linear rounded-lg py-3 px-2 group"
                    >
                        <div className="relative flex items-center space-x-2">
                            <FontAwesomeIcon
                                icon={task.icon}
                                className="w-6 h-6 group-hover:text-[#3ec9a7]"
                            />
                            <div>
                                <p className="font-bold text-base lg:text-lg leading-4 group-hover:text-[#3ec9a7]">
                                    {task.title}
                                </p>
                                {task.description && (
                                    <p className="text-gray-400 text-sm">
                                        {task.description}
                                    </p>
                                )}
                            </div>
                        </div>
                    </a>
                ))}
            </div>
        </div>
    );
};

export default Calendar;